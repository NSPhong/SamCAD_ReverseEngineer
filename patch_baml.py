"""
Patch BAML files: replace Chinese UTF-8 strings with English translations.
BAML format: each string is stored as [7-bit-encoded-length][utf-8-bytes].
We find [len][chinese_bytes] and replace with [new_len][english_bytes].
"""
import os
import sys

SOURCE_DIR = r"D:\Work App\PLC low level\SamCAD\SamCAD\SamCAD\source"

# Full translation mapping for all Chinese strings found in BAML files
TRANSLATIONS = {
    # mainwindow.baml - menu bar
    '文件(_F)': 'File(_F)',
    '编辑(_E)': 'Edit(_E)',
    '视图(_V)': 'View(_V)',
    '项目(_P)': 'Project(_P)',
    '绘图(_D)': 'Draw(_D)',
    '组操作(_G)': 'Group(_G)',
    '图元操作(_I)': 'Entity(_I)',
    '帮助(_H)': 'Help(_H)',
    '新建': 'New',
    '打开': 'Open',
    '保存': 'Save',
    '另存为': 'Save As',
    '撤销(Ctrl+Z)': 'Undo(Ctrl+Z)',
    '恢复(Ctrl+Y)': 'Redo(Ctrl+Y)',
    '剪切(Ctrl+X)': 'Cut(Ctrl+X)',
    '复制(Ctrl+C)': 'Copy(Ctrl+C)',
    '粘贴(Ctrl+V)': 'Paste(Ctrl+V)',
    '新建图像(Alt+P,C)': 'New Image(Alt+P,C)',
    '导入图像(Alt+P,I)': 'Import Image(Alt+P,I)',
    '删除图像(Alt+P,D)': 'Delete Image(Alt+P,D)',
    '调整大小(Alt+P,R)': 'Resize(Alt+P,R)',
    # aboutwindow.baml
    '版权归深圳显控科技股份公司所有': 'Copyright Shenzhen Xiankon Technology Co., Ltd.',
    '未经显控公司授权，复制或抄袭本软件部分或全部内容，将构成侵权行为！': 'Unauthorized reproduction of this software constitutes infringement!',
    '确定': 'OK',
    # createellipsewindow.baml
    '圆心X': 'Center X',
    '圆心Y': 'Center Y',
    '主半径长': 'Major Radius',
    '主半径角度': 'Major Radius Angle',
    '副半径长': 'Minor Radius',
    '副半径角度': 'Minor Radius Angle',
    '方向': 'Direction',
    '顺时针': 'Clockwise',
    '逆时针': 'Counter-clockwise',
    '是否创建这个椭圆？': 'Create this ellipse?',
    '是': 'Yes',
    '否': 'No',
    # createfreerectwindow.baml
    '起点X': 'Start X',
    '起点Y': 'Start Y',
    '主边长': 'Major Side',
    '主边角度': 'Major Side Angle',
    '副边长': 'Minor Side',
    '副边角度': 'Minor Side Angle',
    '使用默认的矩阵方角': 'Default square corners',
    '使用圆角': 'Round corners',
    '圆角半径': 'Corner radius',
    '使用斜角': 'Bevel corners',
    '斜角半径': 'Bevel radius',
    '是否创建这个矩形？': 'Create this rectangle?',
    # createprojectwindow.baml
    '图像名称': 'Image name',
    '文件路径': 'File path',
    '工艺参数': 'Process params',
    '取消': 'Cancel',
    # createrectwindow.baml
    '最左边': 'Left',
    '最上边': 'Top',
    '最右边': 'Right',
    '最下边': 'Bottom',
    # groupexpandwindow.baml
    '变换距离：': 'Transform distance:',
    '向外': 'Outward',
    '向内': 'Inward',
    # groupfillwindow.baml
    '间隔距离': 'Spacing',
    '垂直': 'Vertical',
    '水平': 'Horizontal',
    '环绕': 'Surround',
    '不保留边框': 'No border',
    '示意图': 'Diagram',
    # groupmatrixwindow.baml
    '行数(Y)': 'Rows(Y)',
    '列数(X)': 'Columns(X)',
    '每行X偏移': 'X offset/row',
    '每列Y偏移': 'Y offset/col',
    '虚线方向': 'Dotline direction',
    '螺旋': 'Spiral',
    '折叠': 'Fold',
    '线': 'Line',
    '横纵优先': 'Row/col priority',
    '横向优先(X)': 'Row first(X)',
    '纵向优先(Y)': 'Col first(Y)',
    # groupmovewindow.baml
    '位移=': 'Offset=',
    # groupreorderwindow.baml
    '图元列表内，选中并拖动组项，可以调整相互顺序': 'Select and drag to reorder entities',
    '动作': 'Action',
    '智能优化': 'Smart optimize',
    '以虚线总长度最小为优先': 'Minimize total path length',
    '以虚线拐角平滑为优先': 'Prioritize smooth corners',
    '执行优化': 'Execute',
    '取消优化': 'Cancel optimize',
    # grouprotatewindow.baml
    '原点X=': 'Origin X=',
    '原点Y=': 'Origin Y=',
    '旋转角度=': 'Rotation=',
    # groupscalewindow.baml
    '原点=': 'Origin=',
    '伸缩比=': 'Scale=',
    # imageresizewindow.baml
    '最左边=': 'Left=',
    '最右边=': 'Right=',
    '最下边=': 'Bottom=',
    '最上边=': 'Top=',
    '自动容纳最左侧': 'Auto fit left',
    '自动容纳最右侧': 'Auto fit right',
    '自动容纳最上侧': 'Auto fit top',
    '自动容纳最下侧': 'Auto fit bottom',
    '可以在当前界面看见图形全部': 'View all graphics',
    # inteselectwindow.baml
    '选择全部虚线': 'Select all dotted lines',
    '选择全部实线': 'Select all solid lines',
    '选择全部直线': 'Select all straight lines',
    '选择全部整圆': 'Select all circles',
    '选择全部圆弧': 'Select all arcs',
    # loadmodewindow.baml
    '模式：': 'Mode:',
    '起点以第一个图元开始': 'Start from first entity',
    '工艺参数：': 'Process params:',
    # polylineargumentwindow.baml
    '图像属性': 'Image properties',
    '平面选择编号': 'Plane select no.',
    '运行方式': 'Run mode',
    '仅定位点': 'Position only',
    '运行轨迹': 'Run path',
    '线条属性': 'Line properties',
    '名称': 'Name',
    '目标点': 'Target point',
    '圆心': 'Center',
    '主半径方向': 'Major radius dir',
    '半径': 'Radius',
    '主半径': 'Major radius',
    '副半径': 'Minor radius',
    '是否是实线': 'Is solid line',
    '是否是顺时针方向': 'Is clockwise',
    '等待事件': 'Wait event',
    '等待信号': 'Wait signal',
    '时间': 'Time',
    '脉冲发送完成': 'Pulse complete',
    '执行条件': 'Execute condition',
    '运行中标志': 'Running flag',
    '结束标志位': 'End flag bit',
    '结束传输源': 'End transfer src',
    '结束传输地址': 'End transfer addr',
    '速度': 'Speed',
    '加速时间': 'Accel time',
    '减速时间': 'Decel time',
    '偏移量': 'Offset',
    '批量修改': 'Batch modify',
    '撤销修改': 'Undo changes',
    # polylinedatagridmultiplymodifywindow.baml
    '列名称：': 'Column name:',
    '起始行：': 'Start row:',
    '结束行：': 'End row:',
    '以表格内选中的行为准': 'Use selected rows',
    '修改值：': 'Modify value:',
    '选择全部行': 'Select all rows',
    # polylinedatagridwindow.baml
    '插入': 'Insert',
    '删除': 'Delete',
    '用户参数': 'User params',
    '导入': 'Import',
    '导出': 'Export',
    '编号': 'No.',
    '坐标': 'Coordinate',
    '类型': 'Type',
    '圆心X坐标': 'Center X',
    '圆心Y坐标': 'Center Y',
    # polylineselectwindow.baml
    '智能选择...': 'Smart select...',
    # polylineuserdatasetting.baml
    '添加': 'Add',
    '向上': 'Up',
    '向下': 'Down',
    # radiuspaintlabel.baml
    '半径=': 'Radius=',
    '角度=': 'Angle=',
    # polylineexportcolumnaddwindow.baml
    '添加列：': 'Add column:',
    '系统参数': 'System params',
    # polylineexportwindow.baml
    '开始行': 'Start row',
    '结束行': 'End row',
    '全部': 'All',
    '高级设置': 'Advanced',
    '列信息': 'Column info',
    '上移': 'Move up',
    '下移': 'Move down',
    '元素映射': 'Element mapping',
    '设为': 'Set as',
    '导出到SK': 'Export to SK',
    '导出到：': 'Export to:',
    '屏': 'Screen',
    '上位': 'Host',
    '配方名称': 'Recipe name',
    '配方描述': 'Recipe desc',
    '是否写配方到PLC': 'Write recipe to PLC',
    '是否从PLC读取地址': 'Read addr from PLC',
    # polylineeditor.baml
    '添加虚线': 'Add dotted line',
    '添加线段': 'Add segment',
    '添加圆弧': 'Add arc',
    '添加整园': 'Add circle',
    '添加矩形': 'Add rectangle',
    '添加自由矩形': 'Add free rect',
    '添加椭圆': 'Add ellipse',
    '添加椭圆弧': 'Add ellipse arc',
    '添加B2样条': 'Add B2 spline',
    '图元打断': 'Break entity',
    '图元圆滑': 'Smooth entity',
    '图元锐化': 'Sharpen entity',
    '组合并': 'Group merge',
    '组分割': 'Group split',
    '组移动': 'Group move',
    '组旋转': 'Group rotate',
    '组伸缩': 'Group scale',
    '组镜像': 'Group mirror',
    '组反向': 'Group reverse',
    '组阵列': 'Group array',
    '收缩/扩展': 'Shrink/Expand',
    '线条填充': 'Line fill',
    # replacereportwindow.baml
    '替换结果': 'Replace results',
    '总共进行了114514次替换，1919次成功，810次错误。': 'Total: 114514 replacements, 1919 ok, 810 errors.',
    '我活着，就是罪！': 'I exist, therefore I sin!',
    # floattextbox / rangetextbox
    '最小值': 'Min',
    '最大值': 'Max',
    # themes.baml
    '微软雅黑': 'Microsoft YaHei',
    # Compound strings (ASCII prefix + Chinese) found in BAML
    'SK屏': 'SK Screen',
    'SK上位': 'SK Host',
    'ATC时间': 'ATC Time',
}


def encode_7bit_length(n):
    """Encode integer as 7-bit encoded bytes (BinaryWriter.Write(string) format)."""
    result = []
    while n >= 0x80:
        result.append((n & 0x7F) | 0x80)
        n >>= 7
    result.append(n)
    return bytes(result)


def decode_7bit_length(data, offset):
    """Read 7-bit encoded int from data at offset. Returns (value, bytes_consumed)."""
    result = 0
    shift = 0
    i = 0
    while True:
        b = data[offset + i]
        result |= (b & 0x7F) << shift
        i += 1
        if not (b & 0x80):
            break
        shift += 7
    return result, i


def patch_baml(filepath):
    with open(filepath, 'rb') as f:
        data = bytearray(f.read())

    changes = 0
    for zh, en in TRANSLATIONS.items():
        zh_bytes = zh.encode('utf-8')
        en_bytes = en.encode('utf-8')
        zh_len = len(zh_bytes)
        en_len = len(en_bytes)

        # Find all occurrences of [len_prefix][zh_bytes]
        i = 0
        while i < len(data) - zh_len:
            if data[i:i + zh_len] == zh_bytes:
                # Check that the byte(s) before encode zh_len
                try:
                    pre_val, pre_size = decode_7bit_length(data, i - pre_size_guess(zh_len))
                except:
                    i += 1
                    continue

                # Try to find the length prefix position
                found_prefix = False
                for prefix_offset in [1, 2]:
                    if i - prefix_offset < 0:
                        continue
                    try:
                        val, consumed = decode_7bit_length(data, i - prefix_offset)
                        if val == zh_len and consumed == prefix_offset:
                            # Found the length prefix
                            prefix_start = i - prefix_offset
                            old_chunk = bytes(data[prefix_start:i + zh_len])
                            new_len_bytes = encode_7bit_length(en_len)
                            new_chunk = new_len_bytes + en_bytes
                            data[prefix_start:i + zh_len] = new_chunk
                            changes += 1
                            found_prefix = True
                            # Adjust i for next search
                            i = prefix_start + len(new_chunk)
                            break
                    except Exception:
                        continue

                if not found_prefix:
                    i += 1
            else:
                i += 1

    if changes > 0:
        with open(filepath, 'wb') as f:
            f.write(data)
        return changes
    return 0


def pre_size_guess(string_len):
    """Guess how many bytes the 7-bit encoded length takes."""
    if string_len < 128:
        return 1
    elif string_len < 16384:
        return 2
    return 3


def patch_baml_v2(filepath):
    """
    Robust BAML string patching. Handles two BAML record formats:
      Format A: [len:1][string]           — byte at idx-1 == len(string)
      Format B: [total:1][index:1][string] — byte at idx-2 == len(string)+1,
                                              byte at idx-1 is a per-occurrence index (preserved)
    Both formats use 7-bit encoded length for strings >= 128 bytes.
    """
    with open(filepath, 'rb') as f:
        data = bytearray(f.read())

    changes = 0

    for zh, en in TRANSLATIONS.items():
        zh_bytes = zh.encode('utf-8')
        en_bytes = en.encode('utf-8')
        zh_len = len(zh_bytes)
        en_len = len(en_bytes)

        search_start = 0
        while True:
            idx = bytes(data).find(zh_bytes, search_start)
            if idx == -1:
                break

            if idx < 2:
                search_start = idx + 1
                continue

            ok = False
            prefix_len = 0
            index_byte = None

            # Format A: [len][string] — byte at idx-1 == zh_len
            if data[idx - 1] == zh_len:
                prefix_len = 1
                ok = True
            # Format B: [total][index][string] — byte at idx-2 == zh_len+1
            elif idx >= 2 and data[idx - 2] == zh_len + 1:
                index_byte = data[idx - 1]
                prefix_len = 2
                ok = True
            # 2-byte 7-bit encoded length (for strings >= 128 bytes)
            elif zh_len >= 128:
                b0 = data[idx - 2]
                b1 = data[idx - 1]
                if (b0 & 0x80) and ((b0 & 0x7F) | ((b1 & 0x7F) << 7)) == zh_len:
                    prefix_len = 2
                    ok = True

            if ok:
                prefix_start = idx - prefix_len
                if index_byte is not None:
                    # Format B: preserve index byte, update total length byte
                    new_total = en_len + 1
                    new_chunk = bytes([new_total, index_byte]) + en_bytes
                else:
                    new_len_bytes = encode_7bit_length(en_len)
                    new_chunk = new_len_bytes + en_bytes
                old_end = idx + zh_len
                data[prefix_start:old_end] = new_chunk
                changes += 1
                search_start = prefix_start + len(new_chunk)
            else:
                search_start = idx + 1

    if changes > 0:
        with open(filepath, 'wb') as f:
            f.write(data)

    return changes


def get_baml_files():
    files = []
    for root, dirs, filenames in os.walk(SOURCE_DIR):
        dirs[:] = [d for d in dirs if d not in ('obj', 'bin')]
        for f in filenames:
            if f.endswith('.baml'):
                files.append(os.path.join(root, f))
    return files


if __name__ == '__main__':
    files = get_baml_files()
    print(f"Found {len(files)} BAML files")
    total = 0
    for fpath in files:
        n = patch_baml_v2(fpath)
        if n > 0:
            rel = os.path.relpath(fpath, SOURCE_DIR)
            print(f"  Patched {n} strings: {rel}")
            total += n
    print(f"\nTotal: {total} string replacements across BAML files")
