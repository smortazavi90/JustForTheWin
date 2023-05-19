# JustForTheWin

The test is the following:
Implement a game with the following features:
There are 20 balls in a basket.
Each game turn a player picks a ball.
To pick a ball the player pays 10 credits.
Each ball will give you either a ”win”, ”extra pick” or ”no win” (in case of extra pick, you will draw
another ball with no cost).
From these 20 balls, 5 of them will give you 20 credits (”win” type), 1 of them will give you extra
pick(”extra pick” type) and 14 of them no win (”no win” type).
After each pick you will place the ball back to the basket. After each win the balance of the
player will get updated with the win amount.
You should be able to run/simulate your game with a specified number of rounds* via a player
with unlimited credits and then calculate the RTP (return to player).
RTP = ( (number of won credits) / (number of credits that are spent to play the game))*100.
* One round contains the event(s) that you pay for the current picked ball till(not including) the
next paid picked ball. For example you pay for a pick and after the pick the ball turns out to be
an ”extra pick” ball (first event), then you place the ball back and pick another one for free and
turns out that ball is a ”no win” ball(second event), these two events/picks are considered as
one game round. Following scenario is also a game round with one event/pick: Pay for a pick
and pick a ball then the picked ball is a ”win” ball update the balance.
