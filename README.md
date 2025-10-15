This repository contains a registry for electric vehicles in the state of Washington.
The code is a bit unstructured, but luckily there are some unit tests. 

## Instructions

Clone this repository to your own github account (Register one if necessary).
On your own clone of the repository, solve the exercise. Verify that the code runs, and the test pass.
Make sure everything is committed in the solved state but do _not_ create a pull request back to this repository.
After you are finished, provide a link to your own repository with the solved exercise.
If you have any questions or need clarification, email anders.forsgren at mii.com

## Running the code

Developing the code requires the Dotnet SDK 8.0.
https://dotnet.microsoft.com/en-us/download

OR alternatively, use a full IDE such as Visual Studio
https://visualstudio.microsoft.com/downloads/

To build the code use

> `dotnet build` 

To run the code use

> `dotnet run --project Vehicles <arguments>` 

To execute tests run

> `dotnet test`

## Problems / Excercises

1. The program (and tests) execute too slowly. Can you make it run faster?
1. Taxes can't be calculated for different years at the same time. A tax calculation modifies the data for no good reason.
1. The new tax code for 2025 has arrived. It's not based on type and range like the 2023 and 2024 tax, instead it
   uses a model specific eligibility which is listed in the data.
    - Battery electric vehicles (BEVs) which are "Clean Alternative Fuel Vehicle Eligible": $15
    - Battery electric vehicles (BEVs) which are NOT "Clean Alternative Fuel Vehicle Eligible": $30
    - Plug-in hybrid electric vehicles (PHEVs) which are "Clean Alternative Fuel Vehicle Eligible": $50
    - Plug-in hybrid electric vehicles (PHEVs) which are NOT "Clean Alternative Fuel Vehicle Eligible": $150
    - Any vehicle registred in the city of Seattle gets an additional $7 tax.
    - Any vehicle that is used (Has multiple registrations) gets a tax rebate of -$10.

   It's probably time to refactor the tax calculation logic to support this new tax code.

1. Extend the program so that it takes a County name as an argument, e.g. "Whitman" or "King", and displays the most popular car in that county. If no county is given it should report the most popular car overall. If a county is given that doesn't match any vehicles, tell the user the county they entered likely doesn't exist.


## Troubleshooting

If you have authentication issues with github, it can help to install GitHub Desktop https://desktop.github.com/download/