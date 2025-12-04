<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Http\RedirectResponse;


class LoginController extends Controller
{
    // https://laravel.com/docs/12.x/authentication#authenticating-users the code is taken from here

     public function login(Request $request): RedirectResponse
    {
        $credentials = $request->validate([
            'email' => ['required', 'email'], // meaning its required and must be valid email address format, so something@somethingmail.com
            'password' => ['required'],
        ]);
 
        // "Auth::attempt($credentials)" checks if a user with that email exists and the corresponding hash password matches, laravel automatically compares the entered password with the bcrypt hash (encrypted password), in the table
        if (Auth::attempt($credentials)) { // if it works, sending u to the "dashboard" page, meaning the page only available for the workers
            $request->session()->regenerate(); // Creates a session ID stored in the browser cookie, meaning that when u go into the browser it creates a random session id, is used for safety reasons, such as a person not acting as u and commiting sins, astaghfirullah
            return redirect()->intended('operator'); // if someone tries to access the "dashboard" page, the laravel middleware, says u have to login, if its a success they can continue to the webpage they wanted
        }
 
        // "return back()", people are sent back to the login page, "withErrors" sends a ErrorMessage
        return back()->withErrors([ // failed login attempt
            'email' => 'The provided credentials do not match our records.', // "email" the field that recieves the message, and the other part is the message itself
            
        ]) ->onlyInput('email'); // keeps the email the user typed, meaning the password field will become empty
    
    }
}
