
We have few roles here which need to be assign to the new user

Role Name	Description
Administrator	Full Access to the application
Manager	

Can List Folders, Children, Items, Item Types, Machines

Can Create Folders, Children, Items, Item Types, Machines.

Can Delete Children, Items

Can Edit Children, Items

Can Download files


Member	

Can List Folders, Children, Items, Item Types, Machines

Can Download files


Operator	Can List Folders, Children, Items, Item Types, Machines

Insert credentials for login 

After success login 

In case of invalid credentials you will get next error

Changing Password

We click on Settings and insert current password and twice new password.

On link Folders will open a page only for folders

Pressing button Create on top left will open a page for adding new folder

All fields are mandatory. After pressing save will bring you back on the list of all folders where you will be able to find inserted folder. Code for Folders is automatically generated based on your input in this case from image bellow represent 

Folds: 1, Enter: 1, Exit:1 and sort code of Machine NN = 1_1_1_NN

If you try to insert without value in one of fields, bellow that field you will get error with red text

If we use filters inside of Folders

At any filed when we put value automatically will filter data and display only folders which contain that value, in case above it is displayed every folder which contains 1. Same rules are applied for other filter fields.

If we click on button children

We will get a list of all children related to that specific folder.

In column Action we have 3 buttons, when we move mouse over that buttons display their function

With Editing we can update PLM 

In case of needs more than one PLM code to be inserted we use comma (,) as separator for codes

When we press Download button will download that second file which we had uploaded, and with Delete button will delete from database children and his files including picture.

When we are creating new child, all fields are mandatory and for uploading we must select 2 files, one file is image of 3d part which must be in png or jpg extension saved, 2nd file is file for downloading which will represent the draw 3d part in specific application in this case Solid works.

Item page is same as for folders, only difference is that item doesn't have a child and we put code of item and we have item type to choose, because they can be different, it is not automatically generated. Also, during creation we immediately upload image and file for download.




From machines we use Sort Code only for automatic generating Folders, and for items only to specified for which machine is developed to be used.
