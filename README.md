# File dll và exe ở thư mục gốc là phần mềm gốc
# Các file trong thư mục /source là các mã nguồn được biên dịch ngược
# File thực thi được build nằm trong thư mục \SamCAD\source\SamCAD\bin\x86\Release\net40\SamCAD.exe

---

## Mô tả kiến trúc phần mềm

SamCAD là phần mềm CAD chuyên dụng cho lập trình PLC/HMI, cho phép thiết kế đường polyline (tool path) và xuất dữ liệu sang thiết bị SK. Ứng dụng xây dựng trên WPF (.NET Framework 4.0, x86), gồm 4 tầng rõ ràng.

---

### Cấu trúc Solution (6 projects)

| Project | Vai trò |
|---------|---------|
| `SamCAD` | Ứng dụng chính — giao diện WPF, điều phối lệnh người dùng |
| `SamSoarII.Polyline` | Lõi nghiệp vụ — quản lý entity, undo/redo, export/import |
| `SamSoarII.Dock` | Framework dock/float window cho panel và dialog |
| `SamSoarII.Utility` | Interfaces chung, file format, xử lý DXF |
| `SamSoarII.Dock.resources` | BAML resources cho Dock UI |
| `SamSoarII.Utility.resources` | BAML resources cho Utility UI |

---

### Biểu đồ thành phần (Component Diagram)

```mermaid
graph TD
    subgraph SamCAD ["SamCAD — Presentation Layer"]
        MW[MainWindow]
        UC[UserCommands\n40+ RoutedCommand]
        DLG[Dialog Windows\nCreate/Group/Export/...]
    end

    subgraph Polyline ["SamSoarII.Polyline — Business Logic Layer"]
        PE[PolylineEditor\nDrawing Canvas]
        PROJ[PolylineProject]
        IMG[PolylineImage]
        ENT[PolylineEntity variants\nLine · Arc · Circle · Ellipse · BSpline · Rect]
        EXP[PolylineExportCore\nCSV / SK Device]
        IMP[Import\nCSV / DXF]
    end

    subgraph Dock ["SamSoarII.Dock — UI Framework"]
        DM[DockManager]
        FW[FloatWindow]
        TAB[DockTabSuit]
    end

    subgraph Utility ["SamSoarII.Utility — Infrastructure"]
        IFACE[Interfaces\nIPolylineProject · IPolylineImage · IPolylineEntity]
        FF[FileFormat\n.sca / .ssd binary]
        DXF[DXF Parser]
        ENC[DownloadWriter / UploadReader\nDES Encryption]
    end

    MW --> UC
    MW --> DLG
    MW --> PE
    MW --> PROJ

    PROJ --> IMG
    IMG --> ENT
    PE --> IMG

    EXP --> ENT
    IMP --> IMG

    SamCAD --> Polyline
    SamCAD --> Dock
    Polyline --> Dock
    Polyline --> Utility
    Dock --> Utility

    FF --> ENC
    PROJ -.->|implements| IFACE
    IMG -.->|implements| IFACE
    ENT -.->|implements| IFACE
```

---

### Biểu đồ phụ thuộc giữa các project

```mermaid
graph LR
    SC[SamCAD] --> SP[SamSoarII.Polyline]
    SC --> SD[SamSoarII.Dock]
    SC --> SU[SamSoarII.Utility]
    SP --> SD
    SP --> SU
    SD --> SU
```

> **Quy tắc:** Tầng trên phụ thuộc tầng dưới, không có phụ thuộc ngược. `SamSoarII.Utility` là lõi không phụ thuộc project nào trong solution.

---

### Mô hình dữ liệu (Class Diagram)

```mermaid
classDiagram
    class IPolylineProject {
        <<interface>>
        +string Name
        +string Filename
        +ObservableCollection~IPolylineImage~ Items
        +PolylineArgumentType ArgumentType
        +bool IsDrill
    }

    class IPolylineImage {
        <<interface>>
        +string Name
        +double Left, Top, Width, Height
        +ObservableCollection~IPolylineEntity~ Items
        +ObservableCollection~IPolylineGroup~ Groups
        +IImageArgument Argument
        +List~IPolylineAction~ Undos
        +List~IPolylineAction~ Redos
    }

    class IPolylineEntity {
        <<interface>>
        +PolylineType Type
        +string Name
        +int ID
        +Point From
        +Point To
        +bool IsReal
        +IPolylineGroup Group
        +IPolylineArgument Argument
        +List~IPolylineUserObject~ UserObjs
    }

    class PolylineEntity {
        <<abstract>>
        +Move(Vector)
        +Mirror(Axis)
        +Rotate(Center, Angle)
        +Scale(Center, Factor)
        +Save(FileFormat)
        +Load(FileFormat)
    }

    class PolylineLine { +Point From, To }
    class PolylineArch { +Point Center\n+double Radius\n+double StartAngle, EndAngle\n+bool IsClockwise }
    class PolylineCircle { +Point Center\n+double Radius }
    class PolylineEllipse { +Point Center\n+double MajorAxis, MinorAxis\n+double Rotation }
    class PolylineBSpline { +List~Point~ ControlPoints }
    class PolylineRect { +Point[4] Corners }

    class PolylineGroup {
        +IList~IPolylineEntity~ Items
        +string Name
        +Color Color
    }

    class IPolylineAction {
        <<interface>>
        +OperationType Type
        +Apply()
        +Revert()
    }

    class HMIPLINEArgument {
        +Clone()
        +Save(FileFormat)
        +Load(FileFormat)
    }

    class PolylineExportCore {
        +ObservableCollection~PolylineExportColumn~ Columns
        +int RowStart, RowEnd
        +bool ToSKDevice
        +Export()
    }

    IPolylineProject --> IPolylineImage : contains
    IPolylineImage --> IPolylineEntity : contains
    IPolylineImage --> IPolylineGroup : contains
    IPolylineImage --> IPolylineAction : history
    IPolylineEntity --> IPolylineGroup : belongs to

    PolylineEntity ..|> IPolylineEntity
    PolylineLine --|> PolylineEntity
    PolylineArch --|> PolylineEntity
    PolylineCircle --|> PolylineEntity
    PolylineEllipse --|> PolylineEntity
    PolylineBSpline --|> PolylineEntity
    PolylineRect --|> PolylineEntity

    HMIPLINEArgument ..|> IPolylineArgument
    PolylineExportCore --> IPolylineEntity : reads
```

---

### Mô hình chuỗi Polyline (Polyline Chain)

Các entity được lưu dưới dạng **chuỗi liên tục** — điểm cuối của entity trước là điểm đầu của entity tiếp theo.

```mermaid
graph LR
    SP([StartPoint]) --> E0
    subgraph E0 ["Entity[0] — PolylineLine"]
        F0(From) --> T0(To)
    end
    T0 --> E1
    subgraph E1 ["Entity[1] — PolylineArch"]
        F1(From) --> T1(To)
    end
    T1 --> E2
    subgraph E2 ["Entity[2] — PolylineCircle"]
        F2(From) --> T2(To)
    end

    style SP fill:#4CAF50,color:#fff
    style T2 fill:#F44336,color:#fff
```

- `IsReal = true` → **nét cắt** (tool hạ, cắt vật liệu)
- `IsReal = false` → **đường di chuyển ảo** (tool nâng, di chuyển nhanh)

---

### Sequence Diagram: Vẽ một đường thẳng

```mermaid
sequenceDiagram
    actor User
    participant MW as MainWindow
    participant UC as UserCommands
    participant PE as PolylineEditor
    participant IMG as PolylineImage
    participant UI as Canvas / TreeView

    User->>MW: Click "Draw Line" button
    MW->>UC: Execute DrawLine (RoutedCommand)
    UC->>PE: Activate DrawLine mode
    User->>PE: MouseDown (Point1)
    User->>PE: MouseUp (Point2)
    PE->>IMG: CreateLine(from, to, isReal=true)
    IMG->>IMG: new PolylineLine(from, to)
    IMG->>IMG: Items.Add(line)
    IMG-->>UI: CollectionChanged event
    UI->>PE: Redraw canvas (DrawingAll)
    PE-->>User: Line rendered on screen
```

---

### Sequence Diagram: Lưu project (.sca/.ssd)

```mermaid
sequenceDiagram
    actor User
    participant MW as MainWindow
    participant PROJ as PolylineProject
    participant IMG as PolylineImage
    participant FF as FileFormat
    participant DW as DownloadWriter

    User->>MW: File → Save
    MW->>FF: SaveReset()
    MW->>PROJ: Save(fileFormat)
    loop Mỗi PolylineImage
        PROJ->>IMG: Save(header)
        loop Mỗi PolylineEntity
            IMG->>FF: entity.Save(header)
        end
        loop Mỗi PolylineGroup
            IMG->>FF: group.Save(header)
        end
    end
    FF->>DW: SaveFile(filename)
    DW->>DW: EncryptBegin(key=7)
    DW->>DW: Write binary data
    DW->>DW: [.ssd] Write JPEG thumbnails
    DW->>DW: EncryptEnd()
    DW-->>User: File saved to disk
```

---

### Sequence Diagram: Export dữ liệu ra CSV / SK Device

```mermaid
sequenceDiagram
    actor User
    participant MW as MainWindow
    participant EW as PolylineExportWindow
    participant EC as PolylineExportCore
    participant ENT as PolylineEntity

    User->>MW: Click "Export"
    MW->>EW: Open PolylineExportWindow
    User->>EW: Chọn columns, range, format, file path
    User->>EW: Click "Export"
    EW->>EC: Execute(columns, rowStart, rowEnd)
    loop Mỗi entity trong range
        EC->>ENT: GetCoordinates() → X, Y
        EC->>ENT: GetType() → Type code
        EC->>ENT: GetRadius() / GetCenter() (nếu là Arc/Circle)
        EC->>ENT: GetUserObjs() (custom columns)
        EC->>EC: Write CSV row
    end
    EC-->>User: File .csv / .xlsx saved
```

---

### Các design pattern sử dụng

| Pattern | Nơi áp dụng | Mục đích |
|---------|------------|---------|
| **MVVM** | MainWindow ↔ PolylineProject/Image | Data binding, PropertyChanged tự động cập nhật UI |
| **Command** | `UserCommands` (40+ RoutedCommand) | Tách biệt UI trigger khỏi business logic |
| **Undo/Redo** | `IPolylineAction` + Undos/Redos list | Lịch sử thao tác, khôi phục trạng thái |
| **Factory** | Entity creation by `PolylineType` enum | Tạo đúng loại entity (Line/Arc/Circle/...) |
| **Observer** | `INotifyPropertyChanged`, `ObservableCollection` | UI tự cập nhật khi model thay đổi |
| **Strategy** | MatrixStrategy, ReorderingStrategy, FillStrategy | Hoán đổi thuật toán nhân bản/sắp xếp/điền |
| **Repository** | `FileFormat` + `DownloadWriter`/`UploadReader` | Đóng gói toàn bộ logic đọc/ghi file |

---

### Các định dạng file được hỗ trợ

| Định dạng | Mô tả |
|-----------|-------|
| `.sca` | Binary độc quyền của SamCAD, mã hóa DES |
| `.ssd` | Binary + ảnh thumbnail JPEG nhúng kèm |
| `.dxf` | AutoCAD Drawing Exchange Format (import/export) |
| `.csv / .xlsx` | Export dữ liệu điểm ra bảng, nạp vào thiết bị SK |
