<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class UserSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        // this makes 10 users
        // User::factory(10)->create();
        
        DB::table('users')->insert([
                'email'=>'test@example.com',
                'password'=>Hash::make('password'),
        ]);
    }
}
