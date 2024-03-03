```
HMD - - - - - - - - - - - - - - - - - - - - - - - SERVER

             { "action": "new_room" }
- - - - - - - - - - - - - - - - - - - - - - - - - - >
      { "action": "new_room", "room": ROOMID }
< - - - - - - - - - - - - - - - - - - - - - - - - - -



{ "action": "connect", "name": NAME, "room": ROOMID }
- - - - - - - - - - - - - - - - - - - - - - - - - - >
{ "action": "welcome", "room": ROOMID, "players": [] }
< - - - - - - - - - - - - - - - - - - - - - - - - - -
     { "action": "connected", "player": NAME }
< - - - - - - - - - - - - - - - - - - - - - - - - - -


             { "action": "start" }
- - - - - - - - - - - - - - - - - - - - - - - - - - >
 { "action": "start_scene", "scene": "SampleScene" }
< - - - - - - - - - - - - - - - - - - - - - - - - - -
          { "action": "start_round" }
< - - - - - - - - - - - - - - - - - - - - - - - - - -


{ "action": "round", "name": NAME, "points": 100, "house": 4 }
- - - - - - - - - - - - - - - - - - - - - - - - - - >
{ "action": "round", "name": NAME, "points": 100, "house": 4 }
< - - - - - - - - - - - - - - - - - - - - - - - - - -
          { "action": "start_round" }
< - - - - - - - - - - - - - - - - - - - - - - - - - -
```