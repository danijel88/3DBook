## 3DBook User Guide
We have few roles here which need to be assign to the new user
|Role Name| Description|
| ------------- | ------------- |
|Administrator|Full Access|
| Manager |  Can List Folders, Children, Items, Item Types, Machines. Can Create Folders, Children, Items, Item Types, Machines.Can Delete Children, Items.Can Edit Children, Items.Can Download files |
|Member|Can List Folders, Children, Items, Item Types, Machines.Can Download files|
|Operator|Can List Folders, Children, Items, Item Types, Machines|

### Login
![image](https://github.com/user-attachments/assets/bfd71d7f-8028-4f93-930b-a6decdb77ca0)
After success login
![image](https://github.com/user-attachments/assets/c1caada7-2cab-4f29-bb5a-b136ffd8048f)

In case of invalid credentials you will get next error
![image](https://github.com/user-attachments/assets/dcf2f994-afed-4461-9eed-ff7c12fcebc7)

For changning password please select settings like on picutre bellow.
![image](https://github.com/user-attachments/assets/86b6b7b9-bece-4d6e-9671-385cc4e97ed6)

![image](https://github.com/user-attachments/assets/34c3ef79-edd9-4d55-9446-c23936dc9d7a)

## Usage of application
On link Folders will open a page only for folders
![image](https://github.com/user-attachments/assets/01a69613-b27c-426f-9c0c-5ad963504a9e)
Pressing button Create on top left will open a page for adding new folder
![image](https://github.com/user-attachments/assets/8a2f6e15-b7c7-4c99-a549-078954f8b46d)
All fields are mandatory. After pressing save will bring you back on the list of all folders where you will be able to find inserted folder. Code for Folders is automatically generated based on your input in this case from image bellow represent 

Folds: 1, Enter: 1, Exit:1 and sort code of Machine NN = 1_1_1_NN

![image](https://github.com/user-attachments/assets/19d9d3a6-5b30-4d03-99c3-5553c6b86607)

If you try to insert without value in one of fields, bellow that field you will get error with red text
![image](https://github.com/user-attachments/assets/072cea67-cafc-4be1-9f0c-65e0f0bc5e35)

If we use filters inside of Folders
![image](https://github.com/user-attachments/assets/9c1a6831-63e1-4b98-981f-61bac47c8ca6)

At any filed when we put value automatically will filter data and display only folders which contain that value, in case above it is displayed every folder which contains 1. Same rules are applied for other filter fields.

If we click on button children
![image](https://github.com/user-attachments/assets/41bbab56-64e0-4ddd-9989-90d6b03ee9bd)

We will get a list of all children related to that specific folder.

![image](https://github.com/user-attachments/assets/ef418487-7538-4f1c-a1f0-7bf9ae7f4c2d)

In column Action we have 3 buttons, when we move mouse over that buttons display their function
![image](https://github.com/user-attachments/assets/1afde45d-b1a5-45d0-8449-09409245b155)
![image](https://github.com/user-attachments/assets/34b8c907-95c0-4704-bc88-d580f0d513c5)
![image](https://github.com/user-attachments/assets/edde2763-7f5a-4f6d-9305-41c7ec07d91b)
With Editing we can update PLM 
In case of needs more than one PLM code to be inserted we use comma (,) as separator for codes
![image](https://github.com/user-attachments/assets/f4900f04-8fa1-4cf2-9238-4095bdd5ccac)

When we press Download button will download that second file which we had uploaded, and with Delete button will delete from database children and his files including picture.

When we are creating new child, all fields are mandatory and for uploading we must select 2 files, one file is image of 3d part which must be in png or jpg extension saved, 2nd file is file for downloading which will represent the draw 3d part in specific application in this case Solid works.

![image](https://github.com/user-attachments/assets/84c08627-0823-4807-a86f-967dd2a86e34)
Item page is same as for folders, only difference is that item doesn't have a child and we put code of item and we have item type to choose, because they can be different, it is not automatically generated. Also, during creation we immediately upload image and file for download.
![image](https://github.com/user-attachments/assets/fe101605-59a3-4ee3-99ab-386a2f4c8d4f)
![image](https://github.com/user-attachments/assets/93252ffc-9599-4444-a048-3e0d68c7340d)
From machines we use Sort Code only for automatic generating Folders, and for items only to specified for which machine is developed to be used.
![image](https://github.com/user-attachments/assets/f0ceeddb-7455-4670-8a7b-453267605eb2)
![image](https://github.com/user-attachments/assets/674da7e5-4568-4ef5-ad2b-cf8c9880d417)












