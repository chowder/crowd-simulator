# -*- coding: utf-8 -*-
"""
Created on Tue Jun  4 19:21:04 2019

@author: fridge
"""
from __future__ import annotations
from abc import ABC, abstractmethod
from hashids import Hashids

hashids = Hashids(min_length=5,
                  alphabet='ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789')


"""
Devices are wireless emitting devices
"""


class Device:
    id_count = 0

    def __init__(self):
        self.id = Device.id_count
        self.macaddr = hashids.encode(self.id)
        Device.id_count += 1
        self.callbacks = []
        self.owner = None

    def addCallback(self, callback):
        self.callbacks.append(callback)

    def giveName(self, name: str):
        self.name = name

    def setOwner(self, owner: Person):
        self.owner = owner

    def emit(self):
        for callback in self.callbacks:
            callback(self)


"""
Persons are device carriers
"""


class Person:
    def __init__(self, name: str, x: float, y: float):
        self.name = name
        self.x = x
        self.y = y
        self.devices = []

    def addDevice(self, device: Device):
        self.devices.append(device)
        device.setOwner(self)


"""
Trackers are AP wireless trackers
"""


class Tracker(ABC):
    id_count = 0

    def __init__(self, x: float, y: float):
        self.id = Tracker.id_count
        Tracker.id_count += 1
        self.x = x
        self.y = y

    @abstractmethod
    def alert(self, device: Device):
        pass


class IdealTracker(Tracker):
    def __init__(self, x: float, y: float):
        super().__init__(x, y)

    def alert(self, device: Device):
        print("[Tracker %s] Receiving packet from %s" %
              (self.id, device.macaddr))
