<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\OrderController;
use Illuminate\Routing\Controllers\Middleware;
use App\Http\Controllers\LoginController;

/* When you call the api from frontend REMEMBER!!!!! to have /api at front of /order.. like this http://localhost:8000/api/order
Because /api is prefixed in api routes file */

Route::get('/api/order-system/orders', [OrderController::class, 'getOrders']);

Route::post('/api/order-system/order', [OrderController::class, 'createOrder'])->name('order.makeOrder');


/* Routes for event attendee dynamic views */
Route::get('/attendee', function () { 
    return view('attendee');
}) -> name('goto.attendee');

Route::get('/login', function (){
    return view('login');
})->name('goto.login');

Route::get('/register', function(){
    return view('register');
})->name('goto.register');

Route::post('/login', [LoginController::class, 'login'])->name('login.attempt'); 