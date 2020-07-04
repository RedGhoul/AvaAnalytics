import { store } from '@risingstack/react-easy-state';
import React from 'react';
import Button from '@material-ui/core/Button';
import { Link } from 'react-router-dom';

const WebsiteProvider = store({
    data: [
        {
            name: "Mehmet",
            surname: "Baran",
            birthYear: 1987,
            birthCity: 63,
            metrics: <Button component={Link} to="/metrics" variant="contained">View</Button>
        },
        {
            name: "Mehmet4",
            surname: "Baran",
            birthYear: 1987,
            birthCity: 63,
            metrics: <Button component={Link} to="/metrics" variant="contained">View</Button>
        },
        {
            name: "Mehmet2",
            surname: "Baran",
            birthYear: 1987,
            birthCity: 63,
            metrics: <Button component={Link} to="/metrics" variant="contained">View</Button>
        },
        {
            name: "Mehmet2",
            surname: "Baran",
            birthYear: 1987,
            birthCity: 63,
            metrics: <Button component={Link} to="/metrics" variant="contained" color="primary">View</Button>
        },
    ]

});

export default WebsiteProvider;
