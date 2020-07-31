import React from 'react'

import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import DialogContent from "@material-ui/core/DialogContent";
import DialogTitle from "@material-ui/core/DialogTitle";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemText from "@material-ui/core/ListItemText";

export function CusDialogList(props) {
    const title = props.title;
    const items = props.items;


    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (<React.Fragment>
        <Button variant="outlined" onClick={handleClickOpen}>{title}</Button>

        <Dialog fullWidth={true} open={open} onClose={handleClose} >
            <DialogTitle>{title}</DialogTitle>
            <DialogContent>
                <List>
                    {items.map((item) => (
                        <ListItem key={item}>
                            <ListItemText primary={item} />
                        </ListItem>
                    ))}
                </List>
            </DialogContent>
            <Button onClick={handleClose} color="primary">關閉</Button>
        </Dialog>
    </React.Fragment>);
}