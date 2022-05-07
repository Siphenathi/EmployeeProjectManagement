import React from "react";
import AppBar from '@mui/material/AppBar';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Box from "@mui/material/Box";
import IconButton from "@mui/material/IconButton";
import MenuIcon from '@mui/icons-material/Menu';
import MenuItem from '@mui/material/MenuItem';
import Menu from '@mui/material/Menu';
import Avatar from '@mui/material/Avatar';
import { NavLink, useHistory } from 'react-router-dom';
import Toolbar from '@mui/material/Toolbar';
import Tooltip from "@material-ui/core/Tooltip";
import Drawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MailIcon from '@mui/icons-material/Mail';
import Grid from '@mui/material/Grid';
import {NavigationModel} from '../model/navigationModel';
import './navigation.css';

const pages: NavigationModel [] = [
  {linkName:'Employee', to:'/employee'},
  {linkName:'Project', to:'/project'},
  {linkName:'About', to:'/about'},
  {linkName:'Contact', to:'/contact'}
];
const settings = ['Profile', 'Account', 'Dashboard', 'Logout'];

const NavigationBar = () => {

	const history = useHistory();
    const [anchorElementNav, setAnchorElementNav] = React.useState<null | HTMLElement>(null);
    const [anchorElementUser, setAnchorElementUser] = React.useState<null | HTMLElement>(null);
    
    const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElementNav(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElementNav(null);
    };

    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElementUser(event.currentTarget);
    }

    const handleCloseUserMenu = () => {
        setAnchorElementUser(null);
    };

    return (
        <AppBar position="sticky" className="app-navbar">
          <Container maxWidth="lg">
            <Toolbar disableGutters>
				<NavLink to='/home' key='logo' >					
					<Typography
						variant="h6"
						noWrap
						component="div"
						sx={{ mr: 2, display: { xs: 'none', md: 'flex' }, color:'white' }}
					>
						Employee Project Management		
					</Typography>
				</NavLink>
				<Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
					<IconButton
					size="large"
					aria-label="account of current user"
					aria-controls="menu-appbar"
					aria-haspopup="true"
					onClick={handleOpenNavMenu}
					color="inherit"
					>
					<MenuIcon />
					</IconButton>
					<React.Fragment key='left'>
					<Drawer
						anchor='left'
						open={Boolean(anchorElementNav)}
						onClose={handleCloseNavMenu}
					>
						<List>
							{pages.map((page, index) => (
								<ListItem button key={index} onClick={() => history.push(page.to)}>
									<ListItemIcon>
										{index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
									</ListItemIcon>
									<ListItemText primary={page.linkName} />
								</ListItem>
							))}
						</List>
					</Drawer>
					</React.Fragment>
				</Box>
				<Typography
					variant="h6"
					noWrap
					component="div"
					sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}
				>
					Employee Project Management
				</Typography>
				
              	<Box sx={{ flexGrow: 25, display: { xs: 'none', md: 'flex' } }}/>
				<Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' }, lineHeight:'64px' }}>
					{pages.map((page) => (
					<NavLink to={page.to} key={page.linkName} className="nav-link-style" sx={{backgroundColor:'black'}}>
						<Typography
						variant="h6"
						textAlign="center"
						component="div"
						sx={{ mx: 2, display: { xs: 'none', md: 'flex' , color: 'white'}, lineHeight:'64px', fontSize:'1rem' }}
						>
						{page.linkName}
						</Typography>
					</NavLink>
					))}
				</Box>

				<Box sx={{ flexGrow: 0 }}>
					<Grid container spacing={4}>
						<Grid item xs='auto' md={6}>
							<Tooltip title="Open settings">
							<IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
								<Avatar alt="Remy Sharp" src="/static/images/avatar/2.jpg" />
							</IconButton>
							</Tooltip>
							<Menu
							sx={{ mt: '45px' }}
							id="menu-appbar"
							anchorEl={anchorElementUser}
							anchorOrigin={{
								vertical: 'top',
								horizontal: 'right',
							}}
							keepMounted
							transformOrigin={{
								vertical: 'top',
								horizontal: 'right',
							}}
							open={Boolean(anchorElementUser)}
							onClose={handleCloseUserMenu}
							>
							{settings.map((setting) => (
								<MenuItem key={setting} onClick={handleCloseNavMenu}>
									<Typography textAlign="center">{setting}</Typography>
								</MenuItem>
							))}
							</Menu>
						</Grid>
					</Grid>
				</Box>
            </Toolbar>
          </Container>
        </AppBar>
    );
};

export default NavigationBar;