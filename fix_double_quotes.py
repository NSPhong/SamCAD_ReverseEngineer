"""
Fix C# string literals where translation doubled the surrounding quotes.
Patterns to fix:
  ""text""  ->  "text"     (double quotes on both sides)
  ""text."  ->  "text."    (extra leading quote, partial trailing quote issue)
Also: remove XML-fragment translations (multi-line keys) that got into .cs files.
"""
import re
import os
import json

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

# Build set of translated English values that could appear in .cs string literals
# so we can detect and fix them when surrounded by extra quotes
en_values = set()
for zh, en in mapping.items():
    if en and en != zh and isinstance(en, str) and '\n' not in en:
        en_values.add(en)

# Collect XML-fragment keys (contain \n or resx markup) that shouldn't be in .cs
xml_frag_replacements = []
for zh, en in mapping.items():
    if not en or en == zh:
        continue
    if isinstance(zh, str) and ('\n' in zh or '<value>' in zh or '</data>' in zh):
        xml_frag_replacements.append((zh, en))

print(f"XML fragment translations to undo in .cs: {len(xml_frag_replacements)}")

files = get_cs_files()
total_fixed = 0
files_fixed = 0

# Pattern 1: Fix ""text"" (doubled outer quotes on both sides)
# Pattern 2: Fix ""text" at end of line (extra leading quote)
# Pattern 3: Fix any English translation value that appears with an extra " before it
double_quote_re = re.compile(r'""([^"\n]+)""')
leading_quote_re = re.compile(r'""([^"\n]+)"(?=[,;\)\s\n])')

for fpath in files:
    try:
        with open(fpath, encoding='utf-8', errors='ignore') as f:
            content = f.read()
    except Exception:
        continue

    new_content = content
    fixed_count = 0

    # Fix double-quoted pattern ""text""
    fixed = double_quote_re.sub(lambda m: f'"{m.group(1)}"', new_content)
    if fixed != new_content:
        fixed_count += new_content.count('""') - fixed.count('""')
        new_content = fixed

    # Fix leading extra quote ""text"  (at end of statement)
    fixed = leading_quote_re.sub(lambda m: f'"{m.group(1)}"', new_content)
    if fixed != new_content:
        fixed_count += 1
        new_content = fixed

    # Undo XML-fragment translations that got applied to .cs files
    for zh, en in xml_frag_replacements:
        if en in new_content:
            new_content = new_content.replace(en, zh)
            fixed_count += 1

    if new_content != content:
        with open(fpath, 'w', encoding='utf-8') as f:
            f.write(new_content)
        rel = os.path.relpath(fpath, SOURCE_DIR)
        print(f"  Fixed {fixed_count} patterns: {rel}")
        total_fixed += fixed_count
        files_fixed += 1

print(f"\nFixed {total_fixed} patterns across {files_fixed} files.")
