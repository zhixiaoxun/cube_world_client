using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GameLogic.Network
{
    public enum ToClientCommand
    {
        TOCLIENT_GAME_BREATH = 1,                           // 1
        TOCLIENT_SERVER_READY,                              // 2
        TOCLIENT_ADDNODE,                                   // 3
        TOCLIENT_REMOVENODE,                                // 4
        TOCLIENT_TIME_OF_DAY,                               // 5
        TOCLIENT_BLOCKDATA,                                 // 6
        TOCLIENT_ACCESS_DENIED,                             // 7
        TOCLIENT_PLAYERPOS,                               // 8
        TOCLIENT_ACTIVE_OBJECT_REMOVE_ADD,                  // 9
        TOCLIENT_ACTIVE_OBJECT_MESSAGES,                    // 10
        TOCLIENT_PLAY_SOUND,                                // 11
        TOCLIENT_STOP_SOUND,                                // 12
        TOCLIENT_PLAYER_PITCH_YAW,                                // 13
        TOCLIENT_BLOCKDATA_BATCH,                           // 14
        TOCLIENT_ITEM_DEF,                // 15
        TOCLIENT_NODE_DEF,                        // 16
        TOCLIENT_CRAFT_DEF,                                   // 17
        TOCLIENT_TIME_SYN,                                  // 18
        TOCLIENT_SHOW_TIPS,                                 // 19
        TOCLIENT_NUMBER_EXT_FIELDS_UPDATE,                         // 20
        TOCLIENT_STRING_EXT_FIELDS_UPDATE,                              // 21
        TOCLIENT_MODSDATA,                           // 22
                                                     // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                     // All the protocol command must be added before this line
                                                     // After you add one command, please modify TOSERVER_NUM_MSG_TYPES's Value
                                                     // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        TOCLIENT_NUM_MSG_TYPES = 256,
    };

    enum ToServerCommand
    {
        TOSERVER_LOGIN = 1,                                     // 1
        TOSERVER_CLIENT_READY,                              // 2
        TOSERVER_INTERACT,                                  // 3
        TOSERVER_PLAYERPOS,                                 // 4
        TOSERVER_GET_BLOCK,                              // 5
        TOSERVER_LOGOUT,                                    // 6
        TOSERVER_TIME_SYN,                                  // 7
        TOSERVER_MODSDATA,                              //8
        TOSERVER_DUMMMY5,                               // 9
        TOSERVER_PLAYER_KEYPRESS,                           // 10
                                                            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                                            // All the protocol command must be added before this line
                                                            // After you add one command, please modify TOSERVER_NUM_MSG_TYPES's Value
                                                            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        TOSERVER_MAX_NUM_MSG_TYPES = 256,
    };
}
