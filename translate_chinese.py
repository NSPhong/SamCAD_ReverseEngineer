"""
Translate all Chinese text in SamCAD C# source files to English.
Strategy:
  1. Extract unique Chinese strings from .cs files
  2. Batch-translate using free API (no key needed)
  3. Apply replacements back to all files
"""

import re
import os
import json
import time
import threading
from concurrent.futures import ThreadPoolExecutor, as_completed

SOURCE_DIR = r"D:\Work App\PLC low level\SamCAD\SamCAD\SamCAD\source"
MAPPING_FILE = r"D:\Work App\PLC low level\SamCAD\SamCAD\SamCAD\translation_map.json"

# CJK Unicode range
CHINESE_RE = re.compile(r'[一-鿿㐀-䶿豈-﫿]+')


def has_chinese(text):
    return bool(CHINESE_RE.search(text))


def get_cs_files():
    files = []
    for root, dirs, filenames in os.walk(SOURCE_DIR):
        dirs[:] = [d for d in dirs if d != 'obj' and d != 'bin']
        for f in filenames:
            if f.endswith('.cs') or f.endswith('.resx'):
                files.append(os.path.join(root, f))
    return files


def extract_strings(files):
    """Extract unique Chinese-containing strings from .cs files."""
    strings = set()
    # Match string literals
    str_re = re.compile(r'"([^"\\]*(?:\\.[^"\\]*)*)"')
    # Match comments
    comment_re = re.compile(r'//(.+)$', re.MULTILINE)

    for fpath in files:
        try:
            with open(fpath, encoding='utf-8', errors='ignore') as f:
                content = f.read()
        except Exception:
            continue

        for m in str_re.finditer(content):
            s = m.group(1)
            if has_chinese(s):
                strings.add(s)

        for m in comment_re.finditer(content):
            s = m.group(1).strip()
            if has_chinese(s):
                strings.add(s)

    return sorted(strings)


def translate_batch(texts, translator_func, mapping, save_path, batch_size=30, delay=0.5, workers=4):
    """Translate texts in parallel batches, saving after each batch."""
    lock = threading.Lock()
    total = len(texts)

    def translate_one(text):
        try:
            return text, translator_func(text)
        except Exception as e:
            return text, None

    for i in range(0, total, batch_size):
        batch = texts[i:i + batch_size]
        print(f"  Translating {i+1}-{min(i+batch_size, total)} / {total}...", flush=True)
        with ThreadPoolExecutor(max_workers=workers) as pool:
            futures = {pool.submit(translate_one, t): t for t in batch}
            for fut in as_completed(futures):
                text, result = fut.result()
                mapping[text] = result if result else text
        with open(save_path, 'w', encoding='utf-8') as f:
            json.dump(mapping, f, ensure_ascii=False, indent=2)
        if i + batch_size < total:
            time.sleep(delay)
    return mapping


def apply_translations(files, mapping):
    """Apply translation mapping to all source files."""
    changed = 0
    for fpath in files:
        try:
            with open(fpath, encoding='utf-8', errors='ignore') as f:
                content = f.read()
        except Exception:
            continue

        new_content = content
        for zh, en in mapping.items():
            if zh in new_content and en and en != zh:
                new_content = new_content.replace(zh, en)

        if new_content != content:
            with open(fpath, encoding='utf-8') as f:
                pass  # verify readable
            with open(fpath, 'w', encoding='utf-8') as f:
                f.write(new_content)
            changed += 1
            print(f"  Updated: {os.path.relpath(fpath, SOURCE_DIR)}")

    return changed


def main():
    import translators as ts

    translators_cycle = ['google', 'bing', 'youdao']
    _translator_idx = [0]

    def translate_fn(text):
        last_exc = None
        for _ in range(len(translators_cycle)):
            tr = translators_cycle[_translator_idx[0] % len(translators_cycle)]
            try:
                result = ts.translate_text(text, translator=tr, from_language='zh', to_language='en')
                if result:
                    return result
            except Exception as e:
                last_exc = e
                _translator_idx[0] += 1
        raise last_exc or Exception('all translators failed')

    print("=== SamCAD Chinese → English Translator ===\n")

    # Step 1: Extract
    print("Step 1: Scanning source files...")
    files = get_cs_files()
    print(f"  Found {len(files)} source files")
    strings = extract_strings(files)
    print(f"  Found {len(strings)} unique Chinese strings\n")

    # Step 2: Load existing mapping or start fresh
    if os.path.exists(MAPPING_FILE):
        with open(MAPPING_FILE, encoding='utf-8') as f:
            mapping = json.load(f)
        print(f"  Loaded existing mapping ({len(mapping)} entries)")
        # Only translate strings not yet in mapping
        to_translate = [s for s in strings if s not in mapping]
        print(f"  Need to translate {len(to_translate)} new strings\n")
    else:
        mapping = {}
        to_translate = strings
        print(f"  Starting fresh translation of {len(to_translate)} strings\n")

    # Step 3: Translate
    if to_translate:
        print("Step 2: Translating via Google/Bing/Youdao (free, no API key)...")
        mapping = translate_batch(to_translate, translate_fn, mapping, MAPPING_FILE, batch_size=30, delay=2.0)
        print(f"\n  Saved mapping to: {MAPPING_FILE}")

    # Step 4: Apply
    print("\nStep 3: Applying translations to source files...")
    changed = apply_translations(files, mapping)
    print(f"\nDone! Updated {changed} files.")
    print(f"Translation map saved at: {MAPPING_FILE}")
    print("Run this script again if any strings were missed.")


if __name__ == '__main__':
    main()
