
import React, { useEffect, useState } from "react";
import { observer } from "mobx-react";
import MemberDetails from "../service/MemberDetails";
import VaccinationDetails from "../service/VaccinationDetails";
import CityDetails from "../service/CityDetails"; // Import VaccinationDetails service
import ManufacturerDetails from "../service/ManufacturerDetails";// Import VaccinationDetails service
import TableContainer from '@mui/material/TableContainer';
import Table from '@mui/material/Table';
import TableHead from '@mui/material/TableHead';
import TableBody from '@mui/material/TableBody';
import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';
import Paper from '@mui/material/Paper';
import { Dialog, DialogContent, DialogActions, Button, Typography, IconButton, TextField } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import OpenPanelIcon from '@mui/icons-material/ExpandMore';
import CreateIcon from '@mui/icons-material/Create';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import MenuItem from '@mui/material/MenuItem'; 

const MemberListPage = observer(() => {
    const [data, setData] = useState([]);
    const [selectedMember, setSelectedMember] = useState(null);
    const [selectedMemberAdd, setSelectedMemberAdd] = useState(null);
    const [openDialog1, setOpenDialog1] = useState(false);
    const [openDialog2, setOpenDialog2] = useState(false);
    const [vaccinationData, setVaccinationData] = useState([]); // State for vaccination data
    const [cities, setCities] = useState([]); // State for cities
    const [manufacturers, setmanufacturers] = useState([]); 
    const [filteredData, setFilteredData] = useState([]); 
    const [searchQuery, setSearchQuery] = useState("");


    const [showOnlySickMembers, setShowOnlySickMembers] = useState(false);
    useEffect(() => {
        const fetchData = async () => {
            try {
                const membersData = await MemberDetails.getMember();
                setData(membersData);

                // Fetch vaccination data
                const vaccinationsData = await VaccinationDetails.getVaccinations();
                setVaccinationData(vaccinationsData);


                const citiesData = await CityDetails.getCities();
                setCities(citiesData);

                const manufacturersData = await ManufacturerDetails.getManufacturers();
                setmanufacturers(manufacturersData);

                
            } catch (error) {
                console.error('Error in fetching data:', error);
            }
        };

        fetchData();
    }, []);
    const handleToggleSickMembers = () => {
        setShowOnlySickMembers(prevState => !prevState);
    };

      const filterMembersByName = (member) => {
    return member.name.toLowerCase().includes(searchQuery.toLowerCase());
};
  const filteredMembers = filteredData.filter(filterMembersByName);

  
    useEffect(() => {
        if (showOnlySickMembers) {
            const filteredMembers = data.filter(member => member.startOfIll.trim() !== "");
            setFilteredData(filteredMembers);
        } else {
            setFilteredData(data);
        }
    }, [data, showOnlySickMembers]);

    const handleDialogOpen1 = (member) => {
        setSelectedMember(member);
        setOpenDialog1(true);
    };
   const [typeState, setTypeState]=useState(null)

    const handleDialogOpen2 = (e,member) => {

        const { name } = e.target;
        setTypeState(name);
        setSelectedMemberAdd(member);
        setOpenDialog2(true);
    };

    const handleDialogClose = () => {
        setOpenDialog1(false);
        setOpenDialog2(false);
    };

    const deleteMember = async (id) => {
        try {
            await MemberDetails.deleteMember(id);
            setData(data.filter(member => member.id !== id));
        } catch (error) {
            console.error('Error deleting member:', error);
        }
    };

    const addVaccination = async (newVaccination, handleClose) => {
        try {
            const addedVaccination = await VaccinationDetails.addVaccination(newVaccination);
            // Update the local state with the newly added vaccination
            setSelectedMember(prevState => ({
                ...prevState,
                vaccinations: [...(prevState.vaccinations || []), addedVaccination]
            }));
            handleClose(); // Close the dialog
        } catch (error) {
            console.error("Error adding vaccination:", error);
        }
    };
    const toggleButton = async ( newMember, handleClose) => {
        try {
           
            if (typeState == "newMemberButton") {
                console.log(newMember);
                const addedMember = await MemberDetails.postMember(newMember);
                setData(prevMembers => [...prevMembers, addedMember]);
            } else {
                const updatedMember = await MemberDetails.putMember(newMember);
                setData(prevMembers => prevMembers.map(member => member.id === updatedMember.id ? updatedMember : member));
            }
            handleClose(); 
        } catch (error) {
            console.error("Error:", error);
        }
    };
    
    const emptyMember = {
        // city: { id: "  ", name: " " },
        cityId: " ",
        dateOfBirth: "  ", // Initialize with a valid date object
        endOfIll: "  ", // Initialize with a valid date object
        houseNumber: " ",
        // id: "  ",
        identity: " ",
        mobilePhone: " ",
        name: " ",
        // numOfVaccination: " ",
        phone: "  ",
        startOfIll: "  ", // Initialize with a valid date object
        street: " ",
        // vaccinations: [],
    };
    

    return (
        <div>
              <Button variant="contained" color="primary" onClick={handleToggleSickMembers}>
                {showOnlySickMembers ? "Show All Members" : "Show Only Sick Members"}
            </Button>
                  {/* Search bar */}
      <TextField
        label="Search by Name"
        value={searchQuery}
        onChange={(e) => setSearchQuery(e.target.value)}
        fullWidth
        margin="normal"
      />
             <Button name="newMemberButton" onClick={(event) => handleDialogOpen2(event,emptyMember)}>add member</Button>
            <h1>HMO members</h1>
            <CustomizedTables  data={showOnlySickMembers ? filteredData : data}  handleDialogOpen1={handleDialogOpen1} handleDialogOpen2={handleDialogOpen2} deleteMember={deleteMember} />
            {selectedMember && (
                <CustomizedDialogs open={openDialog1} handleClose={handleDialogClose} selectedMember={selectedMember} addVaccination={addVaccination} manufacturers={manufacturers} />
            )}
            {selectedMemberAdd && (
                <CustomizedDialogs2 open={openDialog2} handleClose={handleDialogClose} setUpdatedMember={setSelectedMemberAdd}selectedMemberAdd={selectedMemberAdd} addVaccination={addVaccination} toggleButton={toggleButton} cities={cities}/>
            )}
        </div>
    );
});

const CustomizedTables = ({ data, handleDialogOpen1, handleDialogOpen2, deleteMember }) => {
    if (!data || data.length === 0) {
        return <p>No data available</p>;
    }

    return (
        <TableContainer component={Paper}>
            <Table aria-label="customized table">
                <TableHead>
                    <TableRow>
                        <TableCell>Identity</TableCell>
                        <TableCell align="right">Name</TableCell>
                        <TableCell align="right">Address</TableCell>
                        <TableCell align="right">Birthday</TableCell>
                        <TableCell align="right">Phone</TableCell>
                        <TableCell align="right"></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {data.map((member, index) => (
                        <TableRow key={index}>
                            <TableCell component="th" scope="row">{member.identity}</TableCell>
                            <TableCell align="right">{member.name}</TableCell>
                            <TableCell align="right">{member.street} {member.houseNumber}{member.city?.name}</TableCell>
                            <TableCell align="right">{member.dateOfBirth}</TableCell>
                            <TableCell align="right">{member.mobilePhone}/{member.phone}</TableCell>
                            <TableCell align="right">
                                <IconButton aria-label="delete" onClick={() => deleteMember(member.id)}>
                                    <DeleteIcon />
                                </IconButton>
                                <IconButton aria-label="open panel" onClick={() => handleDialogOpen1(member)}>
                                    <OpenPanelIcon />
                                </IconButton>
                                <IconButton aria-label="open panel 2" onClick={(event) => handleDialogOpen2(event,member)}>
                                    <CreateIcon />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

const CustomizedDialogs = ({ open, handleClose, selectedMember, addVaccination ,manufacturers }) => {
    const cityName = selectedMember && selectedMember.city ? selectedMember.city.name : '';
    const [producer, setProducer] = useState("");
    const [date, setDate] = useState("");
    const [showTextFields, setShowTextFields] = useState(false); // State to manage visibility of text fields and button
    const [showUpdateDialog, setShowUpdateDialog] = useState(false);
    const handleAddButtonClick = () => {
        if (selectedMember.vaccinations && selectedMember.vaccinations.length < 4) {
            setShowTextFields(true);
        }
        setShowTextFields(true); // Show the text fields and button when "ADD" button is clicked
    };

    const handleAddVaccinationClick = async () => {
        try {
           
            const newVaccination = {
                manufacturerId: producer,
                date: date,
                memberId: selectedMember.id
            };
            // Add the vaccination without closing the dialog
            await addVaccination(newVaccination, () => {});
            // Clear the text fields after adding the vaccination
            setProducer("");
            setDate("");
        } catch (error) {
            console.error("Error adding vaccination:", error);
        }
    };
    const handleUpdateVaccinationClick = async () => {
        try {
            const updatedVaccination = {
                producer: producer,
                date: date,
                memberId: selectedMember.id
            };
            // Update the vaccination without closing the dialog
            await updateVaccination(selectedMember.vaccinations[0].id, updatedVaccination);
        } catch (error) {
            console.error("Error updating vaccination:", error);
        }
    };

    const handleDialogClose = () => {
        setShowTextFields(false); // Hide the text fields and button when dialog is closed
        handleClose();
    };
console.log("selectde",selectedMember);
    return (
        <Dialog onClose={handleDialogClose} open={open} maxWidth="md" fullWidth>
            <DialogContent>
                <Typography variant="h6">Member Details</Typography>
                {/* <Typography>ID: {selectedMember.identity}</Typography> */}
                <Typography>ID: {selectedMember.identity}</Typography>
                <Typography>Name: {selectedMember.name}</Typography>
                <Typography>Street: {selectedMember.street} {selectedMember.houseNumber}</Typography>
                <Typography>City: {selectedMember.city.name}</Typography>
                <Typography>Date of Birth: {selectedMember.dateOfBirth}</Typography>
                <Typography>Phone: {selectedMember.mobilePhone}/{selectedMember.phone}</Typography>
                <Typography>Start of Illness: {selectedMember.startOfIll}</Typography>
                <Typography>End of Illness: {selectedMember.endOfIll}</Typography>
                <Typography variant="h6" style={{ marginTop: '1rem' }}>Vaccination Details</Typography>
                {selectedMember.vaccinations && selectedMember.vaccinations.length > 0 ? (
                    <ul>
                        {selectedMember.vaccinations.map((vaccine, index) => (
                            <li key={index}>
                                Producer: {vaccine.producer}, Date: {vaccine.date}
                                <Button onClick={handleUpdateVaccinationClick}>Update</Button>
                            </li>
                        ))}
                    </ul>
                ) : (
                    <Typography>No vaccination records found.</Typography>
                )}
                {showTextFields && (
                    <div>
                        <TextField
                           select
                           required
                            label="Producer"
                            name="ProducerId"
                            value={producer}
                            onChange={(e) => setProducer(e.target.value)}
                            fullWidth
                            margin="normal"
                        >
                       {manufacturers.map(manufacturer => (
                          <MenuItem key={manufacturer.id} value={manufacturer.id}>
                       {manufacturer.name}
                      </MenuItem>
                        ))}
              </TextField>
                        <TextField
                            label="Date"
                            type="date"
                            value={date}
                            onChange={(e) => setDate(e.target.value)}
                            fullWidth
                            InputLabelProps={{
                                shrink: true,
                            }}
                        />
                        <Button disabled={selectedMember.vaccinations && selectedMember.vaccinations.length >= 4} onClick={handleAddVaccinationClick}>Add Vaccination</Button>
                        {selectedMember.vaccinations && selectedMember.vaccinations.length > 0 && (
                            <Button onClick={handleUpdateVaccinationClick}>Update Vaccination</Button>
                        )}
                    </div>
                )}
            </DialogContent>
            <DialogActions>
                
            {!showTextFields && <IconButton onClick={handleAddButtonClick}><AddCircleOutlineIcon /></IconButton>}
                <Button onClick={handleDialogClose}>Close</Button>
            </DialogActions>
        </Dialog>
    );
};





const CustomizedDialogs2 = ({ open, handleClose, setUpdatedMember, selectedMemberAdd, toggleButton, cities }) => {
    const [photo, setPhoto] = useState(null);
    const [errorMessage, setErrorMessage] = useState('');

    const handlePhotoUpload = (e) => {
        const file = e.target.files[0]; // Get the uploaded file
        setPhoto(file); // Set the photo state
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setUpdatedMember(prevState => ({
            ...prevState,
            [name]: name === 'cityId' ? String(value) : value // Convert cityId value to string
        }));

        // Clear error message when the user starts correcting the fields
        setErrorMessage('');
    };

    // Function to check if the identity number is valid (9 digits)
    const isValidIdentity = (value) => {
        return /^\d{9}$/.test(value);
    };

    // Function to check if the name contains only letters
    const isValidName = (value) => {
        return /^[a-zA-Z]+$/.test(value);
    };
    

    // Function to handle Save button click
    const handleSave = () => {
        if (selectedMemberAdd.identity && !isValidIdentity(selectedMemberAdd.identity)) {
            setErrorMessage('Please enter a valid 9-digit ID card number.');
            return;
        }
        if (selectedMemberAdd.name && !isValidName(selectedMemberAdd.name)) {
            setErrorMessage('Please enter a valid name with letters only.');
            return;
        }
        // If all validations pass or the fields are not filled, save the data
        toggleButton(selectedMemberAdd, handleClose);
    };

    return (
        <Dialog onClose={handleClose} open={open} maxWidth="md" fullWidth>
            <DialogContent>
                <Typography variant="h6">Member Details</Typography>
                <TextField
                    label="Identity"
                    name="identity"
                    value={selectedMemberAdd.identity}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                    inputProps={{
                        pattern: "[0-9]{9}", // Example pattern for a 9-digit ID card
                        title: "Please enter a valid 9-digit ID card number"
                    }}
                />
                <TextField
                    label="Name"
                    name="name"
                    value={selectedMemberAdd.name}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                    inputProps={{
                        pattern: "[A-Za-z]+", // Pattern to allow only letters
                        title: "Please enter a valid name with letters only"
                    }}
                />
                <TextField
                    select
                    label="City"
                    name="cityId"
                    value={selectedMemberAdd.cityId}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                >
                    {cities.map(city => (
                        <MenuItem key={city.id} value={city.id}>
                            {city.name}
                        </MenuItem>
                    ))}
                </TextField>
                <TextField
                    label="Street"
                    name="street"
                    value={selectedMemberAdd.street}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    type="number"
                    label="House Number"
                    name="houseNumber"
                    value={selectedMemberAdd.houseNumber}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    label="Date of Birth"
                    name="dateOfBirth"
                    value={selectedMemberAdd.dateOfBirth}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    label="Phone"
                    name="phone"
                    value={selectedMemberAdd.phone}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    label="Mobile Phone"
                    name="mobilePhone"
                    value={selectedMemberAdd.mobilePhone}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    label="Start of Illness"
                    name="startOfIll"
                    value={selectedMemberAdd.startOfIll}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                />
                {/* Photo upload input field */}
                <input
                    type="file"
                    accept="image/*"
                    onChange={handlePhotoUpload}
                />
                <TextField
                    label="End of Illness"
                    name="endOfIll"
                    value={selectedMemberAdd.endOfIll}
                    onChange={handleInputChange}
                    fullWidth
                    margin="normal"
                />
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>
                <Button onClick={handleSave}>Save</Button>
            </DialogActions>
            {errorMessage && <Typography color="error" align="center">{errorMessage}</Typography>}
        </Dialog>
    );
};
export default MemberListPage;