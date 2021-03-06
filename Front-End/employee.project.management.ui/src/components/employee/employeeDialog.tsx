import { useState, forwardRef } from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Slide from '@mui/material/Slide';
import { TransitionProps } from '@mui/material/transitions';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import {JobTitleModel} from '../model/jobTitleModel';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import {EmployeeModel} from '../model/employeeModel';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
      children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
  ) {
    return <Slide direction="right" ref={ref} {...props} />;
  });

 const EmployeeFormDialog = (props: {openDialog:boolean, setOpenEmployeeDialogCallBack:any, 
                            jobTitles:JobTitleModel[], saveNewEmployeeCallBack: any }) => {

    const [jobTitleId, setJobTitleId] = useState(0);
    const [employeeName, setEmployeeName] = useState('');
    const [employeeSurname, setEmployeeSurname] = useState('');
    const [dateOfBirth, setDateOfBirth] = useState('');

    const handleClose = () => {
        props.setOpenEmployeeDialogCallBack(false);
        clearDialogState();
    };

    const disableSubmitButton = (): boolean => {
        if(jobTitleId === 0 || employeeName === '' 
        || employeeSurname === '' || dateOfBirth === '')
            return true;
        return false;
    }

    const onSubmit = () => {
        handleClose();
        let employeeToBeRegistered : EmployeeModel = 
        {
            name: employeeName, 
            surname: employeeSurname, 
            dateOfBirth: dateOfBirth,
            jobTitleId: jobTitleId, 
            jobTitle:''
        };        
        clearDialogState();
        props.saveNewEmployeeCallBack(employeeToBeRegistered)   
    }

    const clearDialogState = (): void => {
        setJobTitleId(0);
        setEmployeeName('');
        setEmployeeSurname('');
    }

    return (
        <div>
            <Dialog open={props.openDialog} onClose={handleClose} TransitionComponent={Transition}>
                <DialogTitle>
                    <Typography
                        variant="h4"
                        noWrap
                        component="div"
                        align='center'
                    >
                        Register Employee
                    </Typography>            
                </DialogTitle>
                <Divider />
                <DialogContent>
                    <Grid container spacing={2} columns={12}>
                        <Grid item xs={6}>
                            <TextField
                                autoFocus
                                margin="dense"
                                id="name"
                                label="Name"
                                type="email"
                                variant="standard"
                                required={true}
                                onChange={(event: any) => setEmployeeName(event.target.value)}
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <TextField
                                autoFocus
                                margin="dense"
                                id="surname"
                                label="Surname"
                                variant="standard"
                                required={true}
                                onChange={(event: any) => setEmployeeSurname(event.target.value)}
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }} required={true}>
                                <InputLabel id="demo-simple-select-standard-label">Job Title</InputLabel>
                                <Select
                                    labelId="demo-simple-select-standard-label"
                                    id="demo-simple-select-standard"
                                    value={jobTitleId}
                                    onChange={(event: any) => setJobTitleId(Number(event.target.value))}
                                >
                                    {props.jobTitles.map((title) => (
                                        <MenuItem value={title.id} key={title.id}>{title.jobTitle}</MenuItem>
                                    ))}
                                </Select>
                            </FormControl>                            
                        </Grid>
                        <Grid item xs={6}>
                            <TextField
                                    autoFocus
                                    margin="dense"
                                    id="dateofbirth"
                                    label="Date of Birth"
                                    variant="standard"
                                    required={true}
                                    type="date"
                                    onChange={(event: any) => setDateOfBirth(event.target.value)}
                                />                          
                        </Grid>
                    </Grid>            
                </DialogContent>
                <Divider />
                <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>
                <Button variant="contained" onClick={onSubmit} disabled={disableSubmitButton()}>Save</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}

export default EmployeeFormDialog;