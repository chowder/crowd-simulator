# -*- coding: utf-8 -*-
"""
Created on Tue Jun  4 22:18:55 2019

@author: fridge
"""

import pygame as pg
import pygame.gfxdraw as pgfx
from random import randint, uniform
from utils import clamp
vec = pg.math.Vector2

# Variables
WIDTH = 800
HEIGHT = 600
FPS = 60
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
RED = (255, 0, 0)
GREEN = (0, 255, 0)
BLUE = (0, 0, 255)
CYAN = (0, 255, 255)
YELLOW = (255, 255, 0)
DARKGRAY = (40, 40, 40)

# Agent properties
AGENT_SIZE = 32
MAX_SPEED = 3
MAX_FORCE = 0.6
WANDER_RING_DISTANCE = 100
WANDER_RING_RADIUS = 50


class Agent(pg.sprite.Sprite):
    SIZE = 32
    SPEED = 3
    FORCE = 0.6
    W_RING_DISTANCE = 100
    W_RING_RADIUS = 50

    def __init__(self):
        pg.sprite.Sprite.__init__(self, self.groups)
        self.image = pg.Surface((self.SIZE, self.SIZE))
        self.image.fill(YELLOW)
        self.rect = self.image.get_rect()
        self.pos = vec(randint(0, WIDTH), randint(0, HEIGHT))
        self.vel = vec(self.SPEED, 0).rotate(uniform(0, 360))
        self.acc = vec(0, 0)
        self.rect.center = self.pos
        self.last_update = 0
        self.target = vec(randint(0, WIDTH), randint(0, HEIGHT))

    def _seek(self, target):
        self.desired = (target - self.pos).normalize() * MAX_SPEED
        steer = (self.desired - self.vel)
        if steer.length() > self.FORCE:
            steer.scale_to_length(self.FORCE)
        return steer

    def _wander(self):
        future = self.pos + self.vel.normalize() * WANDER_RING_DISTANCE
        target = future + vec(self.W_RING_RADIUS, 0).rotate(uniform(0, 360))
        self.displacement = target
        return self._seek(target)

    def update(self):
        self._move()

    def draw_vectors(self, screen):
        scale = 25
        # vel
        pg.draw.line(screen, GREEN, self.pos,
                     (self.pos + self.vel * scale), 5)
        # desired
        pg.draw.line(screen, RED, self.pos,
                     (self.pos + self.desired * scale), 5)
        # target
        center = self.pos + self.vel.normalize() * self.W_RING_DISTANCE
        pg.draw.circle(screen, WHITE, (int(center.x), int(center.y)),
                       self.W_RING_RADIUS, 1)
        pg.draw.line(screen, CYAN, center, self.displacement, 5)

    def _move(self):
        self.acc = self._wander()
        # equations of motion
        self.vel += self.acc

        if self.vel.length() > self.SPEED:
            self.vel.scale_to_length(self.SPEED)
        self.pos += self.vel

        if not self._inBounds():
            self.pos.x = clamp(self.pos.x, 0, WIDTH)
            self.pos.y = clamp(self.pos.y, 0, HEIGHT)
            self.vel = self.vel.rotate(90)
        self.rect.center = self.pos

    def _inBounds(self):
        return self.pos.x < WIDTH and self.pos.x > 0 and \
               self.pos.y < HEIGHT and self.pos.y > 0


class Person(Agent):
    def __init__(self):
        super().__init__()
        self.devices = []


class Pulse(pg.sprite.Sprite):
    IMG = pg.Surface((30, 30), pg.SRCALPHA)
    pgfx.aacircle(IMG, 15, 15, 14, (0, 255, 0))
    pgfx.filled_circle(IMG, 15, 15, 14, (0, 255, 0))

    ANIM_FRAMES = 10

    def __init__(self):
        pg.sprite.Sprite.__init__(self, self.groups)
        self.image = self.IMG
        self.rect = self.image.get_rect(center=(150, 200))
        self.framesLeft = self.ANIM_FRAMES

    def update(self):
        self.framesLeft -= 1
        if self.framesLeft == 0:
            self.kill()


def main():
    pg.init()
    screen = pg.display.set_mode((WIDTH, HEIGHT))
    clock = pg.time.Clock()

    # Creating and assigning sprite groups
    all_sprites = pg.sprite.Group()
    Agent.groups = all_sprites
    Pulse.groups = all_sprites

    paused = False
    show_vectors = False
    running = True

    while running:
        for event in pg.event.get():
            if event.type == pg.QUIT:
                running = False
                pg.quit()
            if event.type == pg.KEYDOWN:
                if event.key == pg.K_ESCAPE:
                    running = False
                    pg.quit()
                if event.key == pg.K_SPACE:
                    paused = not paused
                if event.key == pg.K_v:
                    show_vectors = not show_vectors
                if event.key == pg.K_m:
                    Person()
                if event.key == pg.K_n:
                    Pulse()

        if not paused:
            all_sprites.update()

        pg.display.set_caption("{:.2f}".format(clock.get_fps()))
        screen.fill(DARKGRAY)
        all_sprites.draw(screen)

        if show_vectors:
            for sprite in all_sprites:
                sprite.draw_vectors(screen)

        pg.display.flip()
        clock.tick(FPS)


if __name__ == "__main__":
    main()
