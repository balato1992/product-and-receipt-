import React, { useState, useEffect } from 'react'

import TextField from '@material-ui/core/TextField';
import Autocomplete from '@material-ui/lab/Autocomplete';

export function CusAutoComplete(props) {

    const value = props.value;
    const handler = props.handler;
    const options = props.options;

    const [v, setV] = useState(value);

    useEffect(() => {

        setTimeout(1, () => {
            setV(value)
        });
    }, [value]);

    return (<React.Fragment>
        <Autocomplete
            fullWidth
            freeSolo
            value={v}
            onChange={(e, newValue) => {
                handler(newValue);
            }}
            onInputChange={(e, newValue) => {
                handler(newValue);
            }}
            options={options}
            renderInput={(params) => <TextField {...params} />}
        />
    </React.Fragment>);
}