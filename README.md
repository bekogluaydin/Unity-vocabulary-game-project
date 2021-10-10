### vocabulary-game-project-
Software Development Laboratory 2 Midterm Project

Most of the assets are from the Asset Store and free assets. So the game may not be very good in terms of UI. My second major game with Unity. The first was the card guessing game. [Unity vocabulary game project link](https://github.com/MyscherzoTR/Unity-vocabulary-game-project) This project was developed for internship 2 and will continue to be developed.

## FireBase

I wanted to use firebase as database. Because it's realtime. My firebase realtime database design is as follows, respectively.

![FireBase](https://user-images.githubusercontent.com/51875713/132956970-ef8f7056-91fc-4812-8b48-096dd0d6d8ec.png)


Users Part; Since it's for Internship 1, I wanted to keep the user part very simple.

![FireBase_kullanıcı](https://user-images.githubusercontent.com/51875713/132956971-702d0928-786a-48d1-b9fc-50ef76e07bb6.png)

Questions and Answers Part; Since it is a quiz game, it consists of questions and answers.

![FireBase_soru](https://user-images.githubusercontent.com/51875713/132956973-415a9bb7-7f03-489d-8370-55bff9e17bb6.png)

Firebase Auth Part;
![FireBase_kullanıcıBilgi](https://user-images.githubusercontent.com/51875713/132956972-451d72fe-504c-4715-a8f3-7e3bfaae4a16.png)


## Main Menu
There are 2 buttons and 2 textbox in the Main Menu. 
1. Giriş Yap (login) and Kayıt ol (register) button
2. Mail and Password text box

![Main Menu](https://user-images.githubusercontent.com/51875713/132955545-7f03684a-59d3-40e2-913b-0a58eb9575c8.png)

# Main Menu Login
On the top right of the main menu, there is the logged in user name and when it is clicked, the profile editing screen appears. In the middle, there are start, add question and standings buttons. Standings is not working. When add question is pressed, adding question section opens.

![Main Menu_Login1](https://user-images.githubusercontent.com/51875713/132955551-5086b8d0-fadc-46ca-bad0-e8e45e16e3ca.png)

## Main Menu Edit Profile
In this section, you can update your name and age information or delete your account.

![Main Menu_EditProfile](https://user-images.githubusercontent.com/51875713/132955549-6613a2d8-836b-4b6d-ad5c-4e209174b323.png)

# Main Menu Add Question
In this section you can add new questions and answers to the Firebase question table.


![Main Menu_AddQuestion](https://user-images.githubusercontent.com/51875713/132955548-2f4be538-156b-4a4c-8037-1384384ca317.png)


## Game Started
When "Start" is pressed in the main menu, this screen opens directly (in game started).

![Oyun Başladı 1](https://user-images.githubusercontent.com/51875713/136702644-08047402-4215-4c26-867d-1a8756d8f60c.png)


# Get A Letter
When the "Harf Al" button is pressed, random a letter are opened in an animated way in the honeycombs below. The "Harf Al" button remains inactive until the letter opening animation ends.
![Harf Al](https://user-images.githubusercontent.com/51875713/136702642-9627a6c9-7fbd-4369-9b78-509f399d7d50.png)

![Harf Al Hakkı Kalmadı](https://user-images.githubusercontent.com/51875713/136702640-70c68334-c1ca-4693-876d-47419368d9ca.png)

# Questin Point
Questions come according to a rule. The answer to the simplest question consists of 4 characters. The hardest is 10. And for each number of characters (such as 4, 5 .... 9,10), there are 2 questions for each. 2 question of 4 characters, then 2 of 5, and this continues up to 10 characters.

The score of the question is the number of characters in the answer multiplied by 100. So a 4-character answer = 4*100 = 400 points.
If "Harf Al" button pressed, 100 points are deducted from unlocked a letter.

![Soru Puan](https://user-images.githubusercontent.com/51875713/136702646-d48c09b7-8063-4c25-ade1-47c3cddb8699.png)
Here, a 7 character question has 4 characters unlocked and the question's score is now 300 instead of 700.
![Soru Puan1](https://user-images.githubusercontent.com/51875713/136702633-2eb300a4-9021-4958-8f13-e35b1100631c.png)



