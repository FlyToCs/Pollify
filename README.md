# 🗳️ Survey System (Console App)

A simple **Survey Management System** built with **.NET** using the **Clean Architecture** approach.  
This project provides an interactive console-based experience with two user roles: **Admin** and **User**.

---

## 🧱 Architecture

This project follows the **Clean Architecture** pattern to ensure:
- High testability  
- Clear separation of concerns  
- Easy maintainability  


---

## 👥 User Roles

### 🛠️ Admin
The admin can:
1. Log in to the system  
2. Create new surveys  
3. Add multiple questions (each question has exactly **4 options**)  
4. Delete surveys  
   - When deleting a survey, all related questions, options, and votes are deleted as well  
   - **Soft Delete** is used instead of permanent deletion  
   - Surveys with active votes cannot be deleted  
5. View survey results, including:
   - List of participants  
   - Total number of participants  
   - Vote count and percentage for each option  

### 🙋 User
The user can:
1. Log in to the system  
2. View available surveys  
3. Participate in a survey (**one vote per survey**)  
4. Cannot vote again after submitting  

---

## 📊 Features

✅ Simple and readable console interface  
✅ Fully modular and layered design  
✅ Entity relationships implemented using EF Core  
✅ Role-based behavior  
✅ Optional: Display voting results as simple text-based charts  

---

## 🧠 Entities Overview

- **Survey**
  - Title, CreatedDate, Questions  
- **Question**
  - Text, SurveyId, Options  
- **Option**
  - Text, QuestionId, Votes  
- **Vote**
  - UserId, OptionId  
- **User**
  - FirstName, LastName, UserName, PasswordHash, Role (Admin / Member)





