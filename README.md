# SecureUserManagement

این ریپازیتوری شامل پیاده سازی کامل مفهوم Authentication (احراز هویت) و Authorization (حق دسترسی) است، از تکنولوژی های JWT Auth، Policy-based، Role-Based Authorization، Refresh Token استفاده شده است.
از دیتابیس SqlLite جهت سبکی استفاده شده است.

---

## هدف پروژه

- پیاده سازی و درک کامل بحث احراز هویت کاربران با تکنولوژی .Net Core 8  
- کمک به آمادگی برای مصاحبه‌های فنی و پروژه‌های واقعی

---

## موضوعات پوشش داده شده
- JWT Auth 
- Role-based + Policy-based  
- Refresh Token
- Revocation (لغو توکن)

---

---

JWT Token شامل سه بخش اصلی است :
1) Header : محل نگهداری الگوریتم رمز نگاری برای امضای توکن است
2) Payload : شامل claim های استاندارد و claim های سفارشی است.
3) Signature : این بخش با الگوریتم مشخص شده در هدر و با استفاده از secret key ساخته میشود / باعث میشود که کسی توکن را نتواند دستکاری کند چون در صورت تغییر این قسمت فیلد میشود.

---

## نحوه استفاده

- کدها در پوشه‌های مربوطه قرار دارند.  
- می‌توانید پروژه‌ها را در محیط Visual Studio یا VS Code اجرا کنید.

---

## مشارکت

- اگر پیشنهادی دارید یا می‌خواهید ویژگی جدیدی اضافه کنید، خوشحال می‌شوم Pull Request یا Issue ارسال کنید.

---

## تماس با من

- ایمیل: m.taghavi.ce@gmail.com 
- لینکدین: https://www.linkedin.com/in/thisismaryamtaghavi/
