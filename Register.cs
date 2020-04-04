using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using Firebase;
using Firebase.Auth;

public class Register : MonoBehaviour {
	public GameObject email;
	public GameObject password;
	public GameObject confPassword;
	private string Email;
	private string Password;
	private string ConfPassword;
	private string form;
	private bool EmailValid = false;
	private string[] Characters = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
								   "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
								   "1","2","3","4","5","6","7","8","9","0","_","-"};
	
	public void RegisterButton(){
		bool EM = false;
		bool PW = false;
		bool CPW = false;

		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

		if (Email != ""){
			EmailValidation();
			if (EmailValid){
				if(Email.Contains("@")){
					if(Email.Contains(".")){
						EM = true;
					} else {
						Debug.LogWarning("Email is Incorrect");
					}
				} else {
					Debug.LogWarning("Email is Incorrect");
				}
			} else {
				Debug.LogWarning("Email is Incorrect");
			}
		} else {
			Debug.LogWarning("Email Field Empty");
		}
		if (Password != ""){
			if(Password.Length > 5){
				PW = true;
			} else {
				Debug.LogWarning("Password Must Be atleast 6 Characters long");
			}
		} else {
			Debug.LogWarning("Password Field Empty");
		}
		if (ConfPassword != ""){
			if (ConfPassword == Password){
				CPW = true;
			} else {
				Debug.LogWarning("Passwords Don't Match");
			}
		} else {
			Debug.LogWarning("Confirm Password Field Empty");
		}
		if (EM == true&&PW == true&&CPW == true)
        {
			auth.CreateUserWithEmailAndPasswordAsync(Email, Password).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
            return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            return;
        }

        // Firebase user has been created.
        Firebase.Auth.FirebaseUser newUser = task.Result;
        Debug.LogFormat("Firebase user created successfully: {0} ({1})",
        newUser.DisplayName, newUser.UserId);
        });
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)){
			if (email.GetComponent<InputField>().isFocused){
				password.GetComponent<InputField>().Select();
			}
			if (password.GetComponent<InputField>().isFocused){
				confPassword.GetComponent<InputField>().Select();
			}
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			if (Password != ""&&Email != ""&&Password != ""&&ConfPassword != ""){
				RegisterButton();
			}
		}
		Email = email.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;
		ConfPassword = confPassword.GetComponent<InputField>().text;
	}

	void EmailValidation(){
		bool SW = false;
		bool EW = false;
		for(int i = 0;i<Characters.Length;i++){
			if (Email.StartsWith(Characters[i])){
				SW = true;
			}
		}
		for(int i = 0;i<Characters.Length;i++){
			if (Email.EndsWith(Characters[i])){
				EW = true;
			}
		}
		if(SW == true&&EW == true){
			EmailValid = true;
		} else {
			EmailValid = false;
		}

	}
}
