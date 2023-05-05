# Time Management

## About

This project was created to manage the work time of a employees in a company. The principal idea is register the time used to an specific Task realized. With that everyone will know the total of hours of a specific task, the evaluate the performanace in the departament.

The project consist in a desktop app (using windows presentation fundation) and sql server for the database. This app will have user account’s to manage the list of task by user. The app has modules to manage the users, and see the information about those.

## Requirements of the project

| Register the task | Register the task do it by an specific user. This task item will have categories, priorities and status. Also will have a creation date. |
| --- | --- |
| Register the time | Register the time used to do an specific task. |
| Count the time in a task | sum the total of hours used to do an task, to comparate the real time used, to the spectation time. |
| Users | The app will have users to administrate the roles, permissions and the access to the modules of the app |
| Reports | The app need to generate reports  to the supervisors, about the users, the task realized and the time to do those. |
| Charts | the app will display charts to understand the data saved in the data base (total of task, time peer task, total of task by category, etc) |

## Class Diagram

This is the diagram of classes propose for the develop of the app:

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/6b13a48f-a403-42fb-8c77-240cdc940edd/Untitled.png)

- **StatusItem**: this class will have the objetive of set the status of the task (if the tasks are complete, are in pending, etc)
- **TaskItem**: this class have the basic information about the task, the name of the task, a litle description of the task, and the date of creation.
- **CategoryItem**: this class have the categories of task (
- **PriorityItem**:
- **TimeItem**:
- **Users**:

# Windows And Descriptions

### Login Page

The app have a simple login page, only require the user and the password. Also the app can remember the data to a better faster login


Note: the user can´t login if is disable. To login need to be enable. Also the  admin characteristics are only enable if the user is an admin system.



### Home

In the home page, the admin have the posibilities to register the task assign to him. Also, in the menu, can view the administrator options (only enable to admin users). IN the middle, the app shows the list of current task assign to the current user. Is possible to see the title, date of creation, status and priority. 

The task in the list, can be edited or disable, if the task is complete. To add a new task is neccessary:

- Title: is a simple name for the task
- Description: a little description about what is the task
- Priority: select the priority.
- Date of Creation: select the day to register the task
- Status: select this to specific the state of the task, for example : is complete, is in review, etc.
- Category: select the category of the task to group the task in the db.


### ViewTaskItem

View task show the times associate to the task. For example, i can add  2 hours from today a specific task. If for some reason, i will work in this task torrow, i will register the time torrow. The page show the times added to this task, and the resume of information (duration in hours and days). Also is possible to add a note about the reason why the time is added.

times added to this task, and the resume of information (duration in hours and days). Also is possible to add a note about the reason why the time is added. /

### List of task Archived

When the task is complete, is possible to mark the task like complete. This action hidde the task from the home page.

The complete list of tasks completed by the user are visible in Task >> Task Archived. In this page is possible to activate a task and show it in the home page.
