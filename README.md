# ATM Console-Based C# Application With OOP (Version 2)
Implemented Object-Oriented Programming like interface, class and object. 

#### Other Versions
- OOP version 1 - https://github.com/ngaisteve1/ATMConsole_With_OOP. This version will have lesser features.
- Database and Entity Framework version - https://github.com/ngaisteve1/ATMConsole_WithDB. This version will have more features.

### Software Development Summary
- Technology: C#
- Framework: .NET Standard, .NET Core 2.2
- Project Type: Class Library (.NET Standard and .NET Core) and Console (.NET Core)
- IDE: Visual Studio Community 2019 (Version 16.1.5)
- Paradigm or pattern of programming: Object-Oriented Programming (OOP)
- Data: Data of this demo program (Bank Account and Transaction data) are stored using List objects. No database is used on purpose for this demo version.
- NuGet: ConsoleTables (Version 2.3.0)

### <img class="emoji" alt="atm" height="20" width="20" src="https://github.githubassets.com/images/icons/emoji/unicode/1f3e7.png"> <img class="emoji" alt="credit_card" height="20" width="20" src="https://github.githubassets.com/images/icons/emoji/unicode/1f4b3.png"> ATM Basic Features / Use Cases:
- [x] Check account balance
- [x] Place deposit
- [x] Make withdraw
- [x] Check card number and pin against bank account list object (Note: No database is used on purpose to demo the use of list object)
- [x] Make third-party-transfer (Transfer within the same bank but different account number)
- [x] View bank transactions

#### Business Rules:
- User is not allow to withdraw or transfer more than the balance amount. A minimum RM20 is needed to maintain the bank account.
- If user key in the wrong pin more than 3 times, the bank account will be locked.
- One user is tied to only one bank account in this version. 

#### Assumption:
- All bank account are the from the same bank

### Activity Diagram
![atm_activity_diagram](https://user-images.githubusercontent.com/21274590/53474610-8615c080-3aa8-11e9-99b2-b1cb32f7ec1c.png)


#### Enhancement (To Do):
- [ ] Fluent Validation to handle input validation of any data type and input length (min, max, fixed)
- [ ] Third party transfer View Model design pattern
- [ ] 'Close' all interface method exposed in main class.

### OOP principles and C# features implemented:
- class (POCO class and utility class)
- enum
- object
- collection initializer
- encapsulation: private, internal and public, field and property
- abstraction: interface
- LINQ to object (LINQ Method syntax)
- List
- static field and static class
- const
- switch case
- string interpolation
- while loop
- foreach loop
- region
- RegionInfo class local currency symbol
- ternary operator ?
- var keyword
- guard clause

### Video Demo
- [ATM Program Demo Video](https://youtu.be/mmbcSJ2FqF0)

[If this content is helpful to you, consider to support and buy me a cup of coffee :) ](https://ko-fi.com/V7V2PN67)
