<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class EventSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        DB::table('event')->insert([
            [
                'Amount_of_people' => '50',
                'Length_of_event' => '5'
            ],
        ]);
        /*
            Event table seeded, now i will create the function where every order refferences to the newest event.
            
        */
    }
}
