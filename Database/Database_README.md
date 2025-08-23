# Sample MSSQL Database

This repository contains a Microsoft SQL Server database dump
including schema and sample data.  
It can be imported into SQL Server using SQL Server Management Studio (SSMS).

---

## 📦 How to Import

1. **Download** `database_dump.sql` from this repository.
2. **Open** SQL Server Management Studio (SSMS).
3. Create a **new query window**.
4. **Open** `database_dump.sql` in SSMS.
5. **Execute** the script — it will:
   - Create the database
   - Create all tables, views, stored procedures, and functions
   - Insert all included sample data

---

## ✅ Requirements
- Microsoft SQL Server 2021 (Express or higher)
- SQL Server Management Studio (SSMS)

---

## 🛠 Notes
- No sensitive information is included — all data is for demo purposes.
- Compatible with default SQL Server collation.
- Security logins/users are **not** included; create them manually if needed.
