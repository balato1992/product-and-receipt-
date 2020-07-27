import React, { useState } from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';

export function Home() {
    const [text, SetText] = useState("");

    const textChanged = (e) => {

        SetText(e.target.value);
    };
    function postClick() {
        fetch("DBVersion", {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(text)
        })
            .then(function (response) {
                return response.text();
            })
            .then((json) => {
                console.log(json);
            })
    }
    function getClick() {
        fetch("DBVersion", {
            method: "GET"
        })
            .then(function (response) {
                return response.text();
            })
            .then((json) => {
                console.log(json);
            })
    }

    return (
        <div>
            <p>Welcome</p>
            <div style={{ display: "none" }}>
                <Button variant="outlined" size="small" onClick={getClick} >Get</Button>
                <Button variant="outlined" size="small" onClick={postClick} >Post</Button>
                <TextField fullWidth onChange={textChanged} />
            </div>
        </div>
    );
}
