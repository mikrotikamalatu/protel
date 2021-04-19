# CKit & Add-Ons Developments for protel HMS

## What is protel CKit?
protel CKit is a development platform to add individual functionality to the protel PMS. From a sales point-of-view, protel CKit is one of the unique selling points of protel PMS, providing a possibility to expand the standard protel functionality at short notice. The range of possible applications covers simple add-ons as well as the most sophisticated enhancements with a fully-fledged HTML user interface.

Now, what do CKit add-ons actually do? Basically, **they modify the database**, e. g. preparing data for reports or storing them permanently for statistics, displaying data to the user, exporting data etc. What is more, protel CKit provides elements to use and **enhance certain genuine protel functionalities**.

Example: the calculation of a reservation forecast with revenue or data for the meal plan. To do this, protel routines are used to calculate a rate code. You can also create **Crystal reports** with protel providing the translation. Or you can open protel dialogs like the reservation dialog, the guest profile or the banquetting event dialogues for a certain profile, reservation or event.

Even though the list of available CKit operations is sparse compared to the all-up functionality of the protel PMS, protel CKit is a flexible and highly effective platform if you want to create individual extentions.

## How to implement protel CKit in protel?
- [x] **Manual Mode**: Allow the user to start an add-on manually, buttons can be inserted in lots of places in protel Front Office, e. g. in the "Add-on" dialog, in the guest profile, the "More ..." dialog of the reservation dialog and many more.
- [x] **Automatic Mode**: A CKit add-on can also be started automatically when certain events take place, e. g. when the end of day procedure is performed, when a reservation is checked in, when a charge is posted, when an invoice is created etc.

## Which programming languages does CKit use?
protel CKit provides a choice of functions rather than being a detailed programming language on itself. To write complex code, other programming languages can be integrated:

- [x] **SQL (Transact-SQL)** is necessary for almost every functionality implemented with protel CKit.
- [x] **VBS** is mainly used to interact with the Microsoft Office products Word and Excel.
- [x] **Web Services** are deployed to communicate data from the script to protel.
- [x] **C++ DLLs** are usually used for tasks, which affect the system performance. As they can establish their own connection to the SQL server, they can be used e. g. to efficiently import data.

## Active Desktop built using CKit
![alt text](https://github.com/mikrotikamalatu/protel/blob/master/ckits/images/AD_Dashboard.png)
