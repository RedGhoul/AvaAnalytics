import { store } from '@risingstack/react-easy-state';
import React from 'react';
import Button from '@material-ui/core/Button';
const WebsiteProvider = store({
    data: [
        {
            name: "Mehmet",
            surname: "Baran",
            birthYear: 1987,
            birthCity: 63,
            actions: <Button variant="contained">Details</Button><Button variant="contained">Details</Button>
        },
        {
            name: "Mehmet4",
            surname: "Baran",
            birthYear: 1987,
            birthCity: 63,
        },
        {
            name: "Mehmet2",
            surname: "Baran",
            birthYear: 1987,
            birthCity: 63,
        },
        {
            name: "Mehmet2",
            surname: "Baran",
            birthYear: 1987,
            birthCity: 63,
        },
    ]

});

export default WebsiteProvider;
