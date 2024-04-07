<img align="center" alt="backgroundWarehouse" width="700px" style="padding-right:10px;" src="https://github.com/OrShitri/Library/blob/master/Library/Images/Welcome.jpg?raw=true" />  

# Library
The library system is a digital library store that simulates a public library.
<br>
The options of the system are a display of the items in the library, rental and return of items, and searching for items according to certain parameters.
<br>
Authors in the library also have the option of editing, adding, deleting, and creating discounts.
<br>
In addition, the system monitors and calculates the library's expenses and income and presents the library's general cash register to authors.
<br>
The library has 2 types of items (Item is an abstract class), a magazine and a book, each of which has its own uniqueness.
<br>
The library has 3 types of users:
<br>
1- User - representing a simple user who logs in with a username and password and can perform actions such as renting and returning items.
<br>
2- Librarian - inherits from a user and has access to various parts of the system that a simple user does not have, such as adding discounts, editing and adding items.
<br>
3- Admin - inherits from a user, and can approve/delete users and has access to all parts of the system.
<br>
In addition, without registration it is possible to log in as a guest and only view partial information about the items in the library.
<br>
All data is saved and loaded automatically when closing/opening the application, on Jason files.
<br>
On the first run of the application, default JSON files are created, in which the administrator's username and password are also saved. 
Username - AdminUser@1991
Password - 123456

>נ
### Dependencies
* .Net Core

### Installing
* Pull from here

### Executing Program

* Compile
* Run the exe
