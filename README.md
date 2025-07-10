# Simple .NET API

## ขั้นตอนติดตั้งและใช้งาน

1. ติดตั้ง .NET SDK
   ดาวน์โหลดได้ที่: [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download)

2. สร้างโปรเจกต์ Minimal API โดยเปิด terminal แล้วรันคำสั่งต่อไปนี้:
   ```bash
   dotnet new webapi -n MyApi
   cd MyApi
   dotnet add package Pomelo.EntityFrameworkCore.MySql

3. สร้างฐานข้อมูลชื่อ testdb จากนั้น import testdb.sql

4. ทดสอบระบบ

- รัน API ทดสอบพร้อมเชื่อมต่อฐานข้อมูล (หรือกด Ctrl + F5)
   ```bash
     dotnet run
