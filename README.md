# 🏋️ PowerFit Gym Management & Retail POS System

An all-in-one desktop application designed to streamline fitness center operations, track memberships, manage trainer schedules, and process storefront retail point-of-sale (POS) transactions. Built natively using C# .NET Windows Forms and Microsoft SQL Server.

---

## 🚀 Core Features

### 🔐 1. Authentication & Session Management
* **Role-Based Routing:** System permissions adapt dynamically based on user roles (`Admin`, `Staff/Cashier`, `Coach`, `Member`).
* **Secure Profile Hub:** Users can modify their own contact profiles from settings, while crucial institutional inputs (Role, Status) are locked automatically.
* **Interactive Privacy Toggle:** Login interface includes a custom, interactive eye-icon toggle to easily view or mask password text characters.

### 💳 2. POS & Membership Renewal Engine
* **Dynamic Timeline Tracker:** Calculates membership renewals seamlessly. If a client's plan is active, new months append to the *future* expiry; if expired, the tracker calculates starting from *today*.

### 📦 3. Sandboxed Inventory Management
* **Portable Asset Archiving:** When adding product images, the application clones the file into a local sandbox subfolder (`\Images\`) and writes relative paths to the database, ensuring the app remains fully portable across different machines.
* **Real-Time Status Filtering:** Live product grids include a toggle checkbox that instantly filters expired or low-stock items out of the dataset using targeted database queries.

### 📊 4. Native Reporting Engine
* **Zero External Dependencies:** Compiles administrative, inventory, and revenue summary logs natively using the `.NET GDI+ Printing Engine` directly to PDF without requiring heavy third-party installations.

---

## 🛠️ Tech Stack & Architecture

* **Frontend:** C# .NET Windows Forms (WinForms)
* **Database Engine:** Microsoft SQL Server (MSSQL Relational DBMS)
* **Data Access Layer:** ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`, `SqlDataAdapter`)
* **Design Pattern:** Layered Component Architecture with Shared Runtime Context (`UserSession`)

---

## 📂 Project Structure

```text
├── Components/          # Shared custom UI control overlays
├── Forms/               # Main application window wrappers
│   ├── loginForm.cs       # Authentication gate and security toggles
│   ├── mainFrame.cs       # Parent viewport housing docked layouts
│   ├── formReports.cs     # Native GDI+ rendering canvas
│   └── ...
├── Images/              # Sandboxed local asset folder (auto-generated)
├── db Script/           # Database backup tracking
│   └── script.sql       # Master relational schema and data seed query
├── dbConnect.cs         # Global ADO.NET connection manager
└── Enums.cs             # Global system type constants
