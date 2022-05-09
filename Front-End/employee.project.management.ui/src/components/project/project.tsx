import { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import TableSortLabel from '@mui/material/TableSortLabel';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import { visuallyHidden } from '@mui/utils';
import {ProjectModel} from '../model/projectModel';
import {ProjectHeadCell} from '../model/projectHeadCell';
import {ProjectEnhancedTableProps} from '../model/projectEnhancedTableProps';
import {SortingOrder} from '../model/sortingOrder';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import CustomAlert from '../shared/customAlert';
import axios from 'axios';
import { AlertModel } from '../model/alertModal';

function descendingComparator<T>(a: T, b: T, orderBy: keyof T) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

function getComparator<Key extends keyof any>(
  order: SortingOrder,
  orderBy: Key,
): (
  a: { [key in Key]: number | string },
  b: { [key in Key]: number | string },
) => number {
  return order === 'desc'
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

function stableSort<T>(array: readonly T[], comparator: (a: T, b: T) => number) {
  const stabilizedThis = array.map((el, index) => [el, index] as [T, number]);
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) {
      return order;
    }
    return a[1] - b[1];
  });
  return stabilizedThis.map((el) => el[0]);
}

const headCells: readonly ProjectHeadCell[] = [
  {
    id: 'name',
    label: 'Name',
  },
  {
    id: 'startDate',
    label: 'Start Date',
  },
  {
    id: 'endDate',
    label: 'End Date',
  },
  {
    id: 'cost',
    label: 'Cost',
  },
  {
    id: 'employees',
    label: 'Employees',
  }
];

const Project = () => {
  const [order, setOrder] = useState<SortingOrder>('asc');
  const [orderBy, setOrderBy] = useState<keyof ProjectModel>('name');
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [searchValue, setSearchValue] = useState('');
  const [projectsData, setProjectsData] = useState([] as ProjectModel[]);
  const [projectsInitialData, setProjectsInitialData] = useState([] as ProjectModel[]);
  const [openAlert, setOpenAlert] = useState(false);
  const [alertDetails, setAlertDetails] = useState({} as AlertModel);

  const EnhancedTableHead = (props: ProjectEnhancedTableProps) => {
	const { order, orderBy, onRequestSort } =
	  props;
	const createSortHandler =
	  (property: keyof ProjectModel) => (event: React.MouseEvent<unknown>) => {
		onRequestSort(event, property);
	  };
  
	return (
	  <TableHead>
		  <TableRow>
			  {headCells.map((headCell) => (
				  <TableCell
					  key={headCell.id}
					  align='right'
					  padding='normal'
					  sortDirection={orderBy === headCell.id ? order : false}
					  sx={{fontWeight:'bold'}}
				  >
					  <TableSortLabel
						  active={orderBy === headCell.id}
						  direction={orderBy === headCell.id ? order : 'asc'}
						  onClick={createSortHandler(headCell.id)}
					  >
						  {headCell.label}
					  {orderBy === headCell.id ? (
						  <Box component="span" sx={visuallyHidden}>
							  {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
						  </Box>
					  ) : null}
					  </TableSortLabel>
				  </TableCell>
			  ))}
		  </TableRow>
	  </TableHead>
	);
  }

  const handleRequestSort = (
    event: React.MouseEvent<unknown>,
    property: keyof ProjectModel,
  ) => {
    const isAsc = orderBy === property && order === 'asc';
    setOrder(isAsc ? 'desc' : 'asc');
    setOrderBy(property);
  };

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const findProject = (event: React.ChangeEvent<HTMLInputElement>) => {
	const enteredName = event.target.value;
	setSearchValue(enteredName);
	var newProjectList = searchProjectByAnyField(enteredName);
	setProjectsData(newProjectList);
}

const searchProjectByAnyField = (name: string): ProjectModel[] => {
	var projectList = projectsInitialData;
	if(name === "" || name === undefined)
		return projectList;

	projectList = projectList.filter(
			item => {
					return (
						item.name.toLowerCase().includes(name.toLowerCase()) || 
						item.employees.toLocaleLowerCase().includes(name.toLocaleLowerCase())
					)
			}
		)
	return projectList;
}

// const fetchAllProjects = () => {
// 	const apiUrl = 'https://localhost:44381/api/v1/project';
//     axios.get(apiUrl)
//         .then(response => {
// 			var results = response.data as ProjectModel[];
// 			setProjectsData(results);
// 			setProjectsInitialData(results);
//         })
// 		.catch(error => {
//             console.log(error);
// 			setAlertDetails({
// 				title: 'Cannot get Projects',
// 				description: error.message,
// 				feedbackType: "error"
// 			});
// 			setOpenAlert(true);
//         })
// }

useEffect(() => {
	console.log('project Data : ', projectsData)
	const fetchData = async () => {
		const apiUrl = 'https://localhost:44381/api/v1/project';
		axios.get(apiUrl)
			.then(response => {
				var results = response.data as ProjectModel[];
				setProjectsData(results);
				setProjectsInitialData(results);
			})
			.catch(error => {
				console.log(error);
				setAlertDetails({
					title: 'Cannot get Projects',
					description: error.message,
					feedbackType: "error"
				});
				setOpenAlert(true);
			})
	}

	fetchData()
  }, []);

const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - projectsData.length) : 0;

  return (
    <Box sx={{ width: '100%' }}>
        <Typography
			variant="h4"
			noWrap
			component="div"
            align='center'
			sx={{ my: 3}}
		>
			Projects
		</Typography>

		<Grid spacing={2} columns={12} sx={{mb:2}} container>
			<Grid item xs={6} container direction="row" justifyContent="center" alignItems="center">
				<TextField 
					id="outlined-basic" 
					label="Search" 
					variant="outlined" 
					size="small"
					onChange={findProject}
					value={searchValue}
				/>
			</Grid>
		</Grid>
      	<Paper sx={{ width: '100%', mb: 2 }}>
			<TableContainer>
				<Table
					aria-labelledby="tableTitle"
					size='medium'
					aria-label="simple table"
				>
					<EnhancedTableHead
						numSelected={8}
						order={order}
						orderBy={orderBy}
						onSelectAllClick={() => {}}
						onRequestSort={handleRequestSort}
						rowCount={projectsData.length}
					/>
					<TableBody>
						{stableSort(projectsData, getComparator(order, orderBy))
							.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
							.map((row, index) => {
								return (
									<TableRow
										hover
										aria-checked={false}
										tabIndex={-1}
										key={row.name}
										selected={false}
									>
										<TableCell component="th" scope="row" align="right">{row.name}</TableCell>
										<TableCell align="right">{row.startDate}</TableCell>
										<TableCell align="right">{row.endDate}</TableCell>
										<TableCell align="right">{row.cost}</TableCell>
										<TableCell align="right">{row.employees}</TableCell>
									</TableRow>
								);
						})}
						{emptyRows > 0 && (
							<TableRow
							style={{
								height:  53 * emptyRows,
							}}
							>
							<TableCell colSpan={6} />
							</TableRow>
						)}
					</TableBody>
				</Table>
			</TableContainer>
			<TablePagination
				rowsPerPageOptions={[5, 10, 25]}
				component="div"
				count={projectsData.length}
				rowsPerPage={rowsPerPage}
				page={page}
				onPageChange={handleChangePage}
				onRowsPerPageChange={handleChangeRowsPerPage}
			/>
      	</Paper>
		<CustomAlert openAlert={openAlert} setOpenAlertCallBack={setOpenAlert} alertDetails={alertDetails}/>
    </Box>
  );
}

export default Project;
