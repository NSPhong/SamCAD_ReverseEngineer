"""
Fix .cs files where translation values with embedded quotes broke string literals.
Pattern: translator returned '"English text."' so "Chinese" became ""English text."
Fix: strip one leading double-quote from the translation in the file.
Also fix translations that contain newlines (multi-line XML fragments in mapping).
"""
import json
import os
import re

SOURCE_DIR = r"D:\Work App\PLC low level\SamCAD\SamCAD\SamCAD\source"
MAPPING_FILE = r"D:\Work App\PLC low level\SamCAD\SamCAD\SamCAD\translation_map.json"

with open(MAPPING_FILE, encoding='utf-8') as f:
    mapping = json.load(f)

def get_cs_files():
    files = []
    for root, dirs, filenames in os.walk(SOURCE_DIR):
        dirs[:] = [d for d in dirs if d not in ('obj', 'bin')]
        for f in filenames:
            if f.endswith('.cs'):
                files.append(os.path.join(root, f))
    return files

# Build set of problematic translations:
# 1. Values that start with " (they added an extra " to C# string literals)
# 2. Values containing newlines (multi-line XML fragments — must not be in .cs strings)
bad_translations = {}
for zh, en in mapping.items():
    if zh == en or not en:
        continue
    en_str = str(en)
    if en_str.startswith('"') or '\n' in en_str:
        # The file contains: " + en_str  (broken)
        # It should contain: " + en_str.lstrip('"') (strip the extra leading quote)
        # OR if newline: the whole thing shouldn't have been applied to .cs
        bad_translations[zh] = (en_str, en_str.lstrip('"').split('\n')[0])

print(f"Found {len(bad_translations)} problematic translations")

files = get_cs_files()
total_fixed = 0
files_fixed = 0

for fpath in files:
    try:
        with open(fpath, encoding='utf-8', errors='ignore') as f:
            content = f.read()
    except Exception:
        continue

    new_content = content
    file_fixed = 0

    for zh, (bad_en, good_en) in bad_translations.items():
        if bad_en in new_content:
            # The bad_en value is already in the file
            # In a C# string context: "bad_en" where bad_en starts with "
            # becomes ""bad_en" (broken). Replace ""bad_en" with "good_en"
            # But we need to be careful not to touch resx/xml files
            bad_pattern = '"' + bad_en  # what's in the file: " + bad_en
            good_pattern = '"' + good_en  # what it should be
            if bad_pattern in new_content and bad_pattern != good_pattern:
                new_content = new_content.replace(bad_pattern, good_pattern)
                file_fixed += 1

    if new_content != content:
        with open(fpath, 'w', encoding='utf-8') as f:
            f.write(new_content)
        rel = os.path.relpath(fpath, SOURCE_DIR)
        print(f"  Fixed {file_fixed} patterns in: {rel}")
        total_fixed += file_fixed
        files_fixed += 1

print(f"\nFixed {total_fixed} patterns across {files_fixed} files.")
