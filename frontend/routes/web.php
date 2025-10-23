<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\OrderController;
use Illuminate\Routing\Controllers\Middleware;

/* When you call the api from frontend REMEMBER!!!!! to have /api at front of /order.. like this http://localhost:8000/api/order
Because /api is prefixed in api routes file */

Route::get('/api/orders', [OrderController::class, 'getOrders']);
Route::post('/api/order', [OrderController::class, 'createOrder']);
