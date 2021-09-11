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
Sample designs for a level(Level 3). As I said, assets are free and i got them from asset store.

![Main Menu_EditProfile](https://user-images.githubusercontent.com/51875713/132955549-6613a2d8-836b-4b6d-ad5c-4e209174b323.png)

# Main Menu Add Question
The climb button is normally deactivated. It becomes active when it comes to the relevant ladder. When the climb button is active, I deactivate the attack and jump buttons to avoid problems such as bugs.


![Main Menu_AddQuestion](https://user-images.githubusercontent.com/51875713/132955548-2f4be538-156b-4a4c-8037-1384384ca317.png)


# Sample Level Game Over Screen
When the character we manage dies (by the enemy or falls down), the endgame panel becomes active.

1. Main Menu; returns to the main menu.
2. RePlay; restarts the active episode (scene).
3. Exit; quit the game

![SampleLevel_gameOverPanel](https://user-images.githubusercontent.com/51875713/132951936-7f637f6a-86ad-43ed-8bdf-23f2d7c98b3f.png)

# Sample Level Next Level Pass
When the chapter is completed successfully, this panel opens while moving to the next chapter.

![SampleLevel_nextLevelPass](https://user-images.githubusercontent.com/51875713/132951938-33f2c8a5-e5a4-4fd5-a8ec-34ddbfef69a4.png)
