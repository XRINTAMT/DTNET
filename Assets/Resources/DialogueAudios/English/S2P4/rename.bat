@echo off
setlocal enabledelayedexpansion

set csvpath=C:\Users\Fedol\OneDrive\Documents\Patient4.csv

FOR /F "usebackq tokens=1,2 delims=," %%g IN (!csvpath!) do (
    set person=%%g
    set name=%%h

    echo My name is !person! and my full name is !name!

    rename !person!.mp3 !name!.mp3
)

pause