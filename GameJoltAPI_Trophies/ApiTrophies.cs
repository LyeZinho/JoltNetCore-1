﻿using GamejoltAPI.Core;
using GamejoltAPI.Users;
using System.Threading.Tasks;


namespace GamejoltAPI.Trophies
{
    public class GJTrophies
    {
        public string game_id;
        public string username;
        public string user_token;
        public string private_key;
        public string apiurl;

        public GJTrophies(GJCore GJCore, GJUser GJUser)
        {
            //make table id and game_id publics
            game_id = GJUser.game_id;
            username = GJUser.username;
            user_token = GJUser.user_token;
            private_key = GJCore.private_key;
            apiurl = GJCore.apiurl;
        }

        public async Task<string> Fetch(bool achieved, string trophy_id)
        {

            string cmd;
            //check if any of the non-required parameters are inputed
            if (achieved==true||achieved==false)
            {
                cmd = "trophies/?game_id=" + game_id + "&username=" + username + "&user_token=" + user_token + "&achieved=" + achieved;
            }
            else
            {
                cmd = "trophies/?game_id=" + game_id + "&username=" + username + "&user_token=" + user_token;
            }
            if (trophy_id != null)
            {
                cmd = "trophies/?game_id=" + game_id + "&username=" + username + "&user_token=" + user_token + "&trophy_id" + trophy_id;
            }
            //if not, then set the default command
            else
            {
                cmd = "trophies/?game_id=" + game_id + "&username=" + username + "&user_token=" + user_token;
            }
            //get signature(signature is the MD5 hash of everything + private_key)
            string fhash = Tools.MD5Hash(cmd + private_key);
            //get actual response(with signature)
            string response = await Tools.Get(apiurl + cmd + "&signature=" + fhash);
            //return response
            return response;
        }


        public async Task<string> Add(string trophy_id)
        {
            //String with paramethers
            string cmd = "trophies/" + "add-achieved/" + "?game_id=" + game_id + "&username=" +
                         username + "&user_token=" + user_token + "&trophy_id=" + trophy_id;


            //Hash of signature 
            string fhash = Tools.MD5Hash(apiurl + cmd + private_key);


            //Response urlApi + paramether + signature
            string response = await Tools.Get(apiurl + cmd + "&signature=" + fhash);


            return response;
        }



        public async Task<string> Remove(string trophy_id)
        {
            //String with paramethers
            string cmd =  "trophies/" + "remove-achieved/" + "?game_id=" + game_id + "&username=" + username +
                            "&user_token=" + user_token + "&trophy_id=" + trophy_id;


            //Hash of signature 
            string fhash = Tools.MD5Hash(apiurl + cmd + private_key);

            
            //Response urlApi + paramether + signature
            string response = await Tools.Get(apiurl + cmd + "&signature=" + fhash);


            return response;
        }



    }
}
