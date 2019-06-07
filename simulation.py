# -*- coding: utf-8 -*-
"""
Created on Tue Jun  4 21:51:33 2019

@author: fridge
"""
from objects import Person


class Simulation:
    def __init__(self):
        self.persons = []

    def addPerson(self, person: Person):
        self.persons.append(person)

    def _step(self):
        for person in self.persons:
            person.step(1)
