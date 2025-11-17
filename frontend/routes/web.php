<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\OrderController;
use App\Http\Controllers\MachineController;
use Illuminate\Routing\Controllers\Middleware;
use App\Http\Controllers\LoginController;
use Illuminate\Http\Request;

/* When you call the api from frontend REMEMBER!!!!! to have /api at front of /order.. like this http://localhost:8000/api/order
Because /api is prefixed in api routes file */

/* operator page start 

Route::get('/operator/status', function () {
    // Example dynamic data — replace with DB or C# layer input
    return response()->json([
        'status' => cache('machine_status', 'Idle'),
        'current_item' => cache('current_item', 'N/A'),
        'total_produced' => cache('total_produced', 0),
    ]);
});

Route::post('/operator/start', function () {
    cache(['machine_status' => 'Running']);
    return response()->json(['message' => 'Machine started']);
});

Route::post('/operator/stop', function () {
    cache(['machine_status' => 'Stopped']);
    return response()->json(['message' => 'Machine stopped']);
});

*/ //Operator page done

Route::get('/api/orders', [OrderController::class, 'getOrders']);
Route::post('/api/order', [OrderController::class, 'createOrder']);
Route::get('/', function () {
    return view('index');
})->name('index');
Route::get('/machine', function () {
    return view('machine');
})->name('machine');

Route::get('/operator', function () {
    return view('machine');
})->name('operator');

Route::get('/api/order-system/orders', [OrderController::class, 'getOrders']);

Route::post('/api/order-system/order', [OrderController::class, 'createOrder'])->name('order.makeOrder');


/* Routes for event attendee dynamic views */
Route::get('/attendee', function () {
    return view('attendee');
})->name('goto.attendee');

// Frontend operator
Route::post('/connect', [MachineController::class, 'connectToMachine'])->name('connect');
Route::post('/power', [MachineController::class, 'Power'])->name('Power');

Route::get('/login', function () {
    return view('login');
})->name('goto.login');

Route::get('/register', function () {
    return view('register');
})->name('goto.register');

Route::post('/login', [LoginController::class, 'login'])->name('login.attempt');
