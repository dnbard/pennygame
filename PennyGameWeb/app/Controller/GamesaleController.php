<?php
/**
 * Created by PhpStorm.
 * User: alexbard
 * Date: 03.12.13
 * Time: 0:09
 */

class GamesaleController extends AppController{
    public $helpers = array('Html', 'Form');

    public function index() {
        $this->set('gamesale', $this->Gamesale->find('all'));
    }
}