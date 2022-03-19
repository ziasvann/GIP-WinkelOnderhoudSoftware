# GIP-WinkelOnderhoudSoftware
### ToDo:

- Duidelijker documenteren
- Betere foutafhandeling
- Kassa

### Bugs: 

- Bij het invullen van de prijs kun je geen kommagetal invullen


### Done:

- Bugfix: "Als je tijdens het wijzigen een product selecteerd blijf je het vorige product wijzigen" 
--> Bij selectedIndexChanged van lvProducten wordt er gevraagd of er moet worden verder gewerkt worden, dan blijft het oude item enkel geselecteerd. Anders wordt de panel bewerken onzichtbaar gemaakt. Dan wordt het nieuwe item ook geselecteerd.
