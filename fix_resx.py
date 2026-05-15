"""
Fix .resx files corrupted by naive string replacement.
The translation replaced Chinese text in XML name= attributes.
Strategy: restore original Chinese in name= attrs, keep <value> content as-is.
"""
import re
import json
import os
import sys

MAPPING_FILE = r"D:\Work App\PLC low level\SamCAD\SamCAD\SamCAD\translation_map.json"

with open(MAPPING_FILE, encoding='utf-8') as f:
    zh_to_en = json.load(f)

# Build reverse map: en -> zh (for restoring corrupted name attrs)
en_to_zh = {}
for zh, en in zh_to_en.items():
    if en and en != zh:
        # Only add if not already present (first mapping wins)
        if en not in en_to_zh:
            en_to_zh[en] = zh

RESX_FILES = [
    r"D:\Work App\PLC low level\SamCAD\SamCAD\SamCAD\source\SamSoarII.Dock.resources\SamSoarII.Dock.Properties.Resources.zh-Hans.resx",
    r"D:\Work App\PLC low level\SamCAD\SamCAD\SamCAD\source\SamSoarII.Utility\SamSoarII.Utility.Properties.Resources.resx",
    r"D:\Work App\PLC low level\SamCAD\SamCAD\SamCAD\source\SamSoarII.Utility.resources\SamSoarII.Utility.Properties.Resources.zh-Hans.resx",
]

def fix_resx(fpath):
    if not os.path.exists(fpath):
        print(f"  SKIP (not found): {fpath}")
        return

    with open(fpath, encoding='utf-8', errors='ignore') as f:
        content = f.read()

    # Step 1: Restore the raw bytes for <data name="..."> lines
    # The corruption pattern: translation text (possibly with quotes) ended up in name attr.
    # We rebuild each <data ...> tag by reversing translations in the name= attribute only.

    # First pass: fix broken data tags where quotes got embedded.
    # Pattern: <data name=" followed by anything up to the next xml:space or >
    # We need to handle the case where the name attr value was corrupted with extra quotes.
    # Strategy: read line by line, detect <data name= lines, try to restore.

    lines = content.split('\n')
    fixed_lines = []
    restored = 0

    for line in lines:
        # Check if this is a <data name="..." line
        if '<data name=' not in line:
            fixed_lines.append(line)
            continue

        # Try to find the name attribute value - may be broken
        # Pattern 1: normal  <data name="VALUE" xml:space=...>
        # Pattern 2: broken  <data name=""VALUE" xml:space=...>  (extra leading quote)
        # Pattern 3: broken  <data name="VALUE WITH SPACE" ...>  (spaces in name - valid XML but wrong)

        # Extract everything between <data name=" and the xml:space or > that follows
        # We use a greedy approach: find <data name=" then read until we hit xml:space or />
        m = re.match(r'^(\s*<data name=")(.*?)(" xml:space="preserve">.*)', line, re.DOTALL)
        if not m:
            # Try without xml:space
            m = re.match(r'^(\s*<data name=")(.*?)(".*>.*)', line, re.DOTALL)

        if not m:
            fixed_lines.append(line)
            continue

        prefix, name_val, suffix = m.group(1), m.group(2), m.group(3)

        # Check if name_val looks like it was translated (is in en_to_zh)
        # Also check for corruption patterns:
        # - Starts with " (extra quote leaked in)
        # - Contains spaces where the English translation has spaces
        original_name = name_val

        # Remove any leading/trailing stray quotes from corruption
        clean_name = name_val.lstrip('"').rstrip('"')

        # Try to reverse-lookup: is this an English translation of a Chinese string?
        restored_zh = None
        if clean_name in en_to_zh:
            restored_zh = en_to_zh[clean_name]
        elif name_val in en_to_zh:
            restored_zh = en_to_zh[name_val]

        if restored_zh:
            # Restore original Chinese in the name attribute
            fixed_line = prefix + restored_zh + suffix
            fixed_lines.append(fixed_line)
            restored += 1
        else:
            # No match - keep as-is but fix obvious quote corruption
            if name_val.startswith('"'):
                # Strip the extra leading quote
                fixed_line = prefix + clean_name + suffix
                fixed_lines.append(fixed_line)
                restored += 1
            else:
                fixed_lines.append(line)

    fixed_content = '\n'.join(fixed_lines)

    if fixed_content != content:
        with open(fpath, 'w', encoding='utf-8') as f:
            f.write(fixed_content)
        fname = os.path.basename(fpath)
        print(f"  Fixed {restored} name attrs in: {fname}")
    else:
        print(f"  No changes needed: {os.path.basename(fpath)}")


print("Fixing corrupted .resx files...")
for fpath in RESX_FILES:
    fix_resx(fpath)
print("Done.")
