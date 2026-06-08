using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicMultiCommander.Methods
{
    class StaticData
    {
        //玩家角色guid
        public const string playerActorBlueprintTypeGUID = "4391e8b9afbb0cf43aeba700c089f56d";
        //玩家角色guid，含预制角色
        public static List<String> playerActorBlueprintTypeGuidList = ["4391e8b9afbb0cf43aeba700c089f56d",
            "57c2aaeb11ee4f8d81f0a57974a94f1b",
            "1f6d72fd52ce418fb677db2243ea4de5",
        "2160635e9bba4b9e81a5cfcd45e3d141",
        "fad59e6db3aa470ca7e8962e2daa12dc",
        "8abbe46e26844e02a39645ae34913612",
        "d27dd725873142039c6015fbf49ac621"];

        //PlayerIs 道途Etude
        public static List<String> mythicGuidEtudes = ["3a82aba4de71b89458ac82949ed957c4",
            "3a040afde22f4b742a2f607354ab17e7",
            "d3b47e973d65c6c46af1cce815d1f6ce",
            "9a3739370f84b0b4196d0e4d326ea3a8",
            "a1db9daf676b36f4d99c6a788a0fe1af",
            "9b193d30c89a20b409fd3dda9bd109bf",
            "c6165efcd5571c442ae38d7c0601f2df",
            "11fc5662e0ce8074ea145a022282b879",
        "439e63fed37f52048887d98f99255e40",
        "9f486a9c0c9abfc4a952bb22e88a7e96"];

        //Path 道途Etude
        public static List<String> mythicDeepGuidEtudes = ["ef9a0bf19a33b0a4ea1b777b1024f428",
            "3a25b1f2a81f84a40b4fb658f4d2fe0f",
            "0289be5aa42821e4499415b58507efac",
            "7a604dfb35c17c9468e25f1656e0a9fe",
            "7b80ec84c75e76e4e9c8a45fefca34b3",
            "9b193d30c89a20b409fd3dda9bd109bf",
            "c6165efcd5571c442ae38d7c0601f2df",
            "11fc5662e0ce8074ea145a022282b879",
        "439e63fed37f52048887d98f99255e40",
        "9f486a9c0c9abfc4a952bb22e88a7e96"];

        //Mythic** 道途Etude
        public static List<String> mythicPathGuidEtudes = ["891dbd7391889b2429b64a20bdfcc13a",
            "b02407626a98d204191a528cd8b5de6c",
            "c96c9112fa1b61d428c8cc8f57dfa439",
            "d6464b0006ebd284980494bbcea992ef",
            "061c645b5a7ff1b4a9574e41c0c6541a",
            "b570e6928ad43ee489d2fefd79b86a98",
            "99043bff468a8ed47b65d68c917b52d3",
            "3002b84a0cfe89845903e053738fe6cd",
        "4da0ddbe8fb98294cb1826989ab77e4a",
        "c9de3e00b166802448f1ba635fb791ab"];

        //神话道途职业
        public static List<String> mythicClassGuid = ["b82f1fbd191e1f2498266ca41f05027f",
            "3d420403f3e7340499931324640efe96",
            "a5a9fe8f663d701488bd1db8ea40484e",
            "5d501618a28bdc24c80007a5c937dcb7",
            "15a85e67b7d69554cab9ed5830d0268e",
            "8e19495ea576a8641964102d177e34b7",
            "9a3b2c63afa79744cbca46bea0da9a16",
            "247aa787806d5da4f89cfc3dff0b217f",
        "5295b8e13c2303f4c88bdb3d7760a757",
                    "daf1235b6217787499c14e4e32142523",
                    "211f49705f478b3468db6daa802452a2"];

        //伊兹城回归对话
        public static List<String> cornoationcueGuid = ["5b07b88e66521b841a124246ddde1d13",
            "0010d97a910a5474396bf02d8b572ddf",
            "8ee8082e9ea166e4cbfa1e083fb12fbd",
            "8e3ccbfbf3f6d2b4b95dc3c4c251288c",
            "d1f852f89951bd5418f3f6f60676b337",
            "e914c86811a2c1240977e032a3e38a01",
            "4e710afd50ff96346bd363b8744ac071",
            //"3d75ae2e83db0084396b1a2c71775929",
        "beb480337af160f489e80dc70cf019b2",
                    "7bb22bab875cb2b4b8cb74ec397cf26d"];
        //浪漫剧情角色绑定
        public static List<String> romanceGuid = ["fe6ee37b2aa394e4eaa51208cf7d7f86",
            "8541453b31379964e834cf2309444388",
            "af394a43ba7c1314bb31aea74a5e4c0e",
            "14650554734f4adc81af61e442fd628b",
            "efb130b8a22c9534ca40a1e41ef8e931",
            "6a3fdd0758fe78d4aa2c3b26d7614fbc",
            "e49702f590611644580f09c8f9ef0e5b",
            "33c4c2f66f2461e4993df21566252079"];

        public static List<String> romanceAreaMechnicaGuid = ["c7b51ac4d31ec0d44a9bf247970392d9",
            "ce2576c45ec0d0248910c8527eced895",
            "e5940f2cfee4beb46944a7e3e7e95ab6",
            "3e5ac276944aca344a5af1c876349668",
            "644baac64a1af7342958baff0bcde601",
            "36651f28bb1e4bbe8abb7ee4e931d56c",
            "d53e29755de89ec48842a15581aa869c"];
    }
}
