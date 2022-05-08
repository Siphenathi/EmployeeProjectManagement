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
import {EmployeeModel} from '../model/employeeModel';
import {EmployeeHeadCell} from '../model/employeeHeadCell';
import {EmployeeEnhancedTableProps} from '../model/employeeEnhancedTableProps';
import {SortingOrder} from '../model/sortingOrder';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import EmployeeFormDialog from './employeeDialog';
import {JobTitleModel} from '../model/jobTitleModel';

function createData(
    name: string,
    surname: string,
	dateOfBirth: string,
    jobTitle: string,
	jobTitleId: number
  ) : EmployeeModel {
    return { name, surname, dateOfBirth, jobTitle, jobTitleId };
  }

const rows = [
    createData('Thabani', 'Ntembe', '04/02/1990', 'Software Developer', 1),
    createData('Sindisiwe', 'Kubheka', '04/02/1992', 'Software Tester', 2),
    createData('Nosipho', 'Gumede', '04/02/1993', 'Database Administrator', 3),
    createData('Sipho', 'Gumede', '04/02/1994', 'Database Administrator', 4),
    createData('Zuko', 'Gumede', '04/02/1995', 'Database Administrator', 5),
    createData('Nathi', 'Gumede', '04/02/1996', 'Database Administrator', 6),
    createData('Olwethu', 'Gumede', '04/02/1997', 'Database Administrator', 7),
    createData('Funeka', 'Gumede', '04/02/1998', 'Database Administrator', 8),
    createData('Hlalanathi', 'Gumede', '04/02/1999', 'Database Administrator', 9),
    createData('Ncebakazi', 'Gumede', '04/02/2000', 'Database Administrator', 10),
    createData('Andisiwe', 'Gumede', '04/02/2001', 'Database Administrator', 11),
    createData('Sicelo', 'Gumede', '04/02/2002', 'Database Administrator', 12),
    createData('Mandla', 'Gumede', '04/02/2003', 'Database Administrator', 13)
];

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

const headCells: readonly EmployeeHeadCell[] = [
  {
    id: 'name',
    label: 'Name',
  },
  {
    id: 'surname',
    label: 'Surname',
  },
  {
    id: 'dateOfBirth',
    label: 'Date of Birth',
  },
  {
    id: 'jobTitle',
    label: 'JobTitle',
  }
];

const Employee = () => {
  const [order, setOrder] = useState<SortingOrder>('asc');
  const [orderBy, setOrderBy] = useState<keyof EmployeeModel>('name');
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [searchValue, setSearchValue] = useState('');
  const [employeesData, setEmployeesData] = useState(rows);
  const [openEmployeeDialog, setOpenEmployeeDialog] = useState(false);
  const [jobTitles, setJobTitles] = useState([] as JobTitleModel[])

  const EnhancedTableHead = (props: EmployeeEnhancedTableProps) => {
	const { order, orderBy, onRequestSort } =
	  props;
	const createSortHandler =
	  (property: keyof EmployeeModel) => (event: React.MouseEvent<unknown>) => {
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
    property: keyof EmployeeModel,
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

  const findEmployee = (event: React.ChangeEvent<HTMLInputElement>) => {
	const enteredName = event.target.value;
	setSearchValue(enteredName);
	var newEmployeeList = searchEmployeeByAnyField(enteredName);
	setEmployeesData(newEmployeeList);
}

const searchEmployeeByAnyField = (name: string): EmployeeModel[] => {
	var employeeList = rows as unknown as EmployeeModel[];
	if(name === "" || name === undefined)
		return employeeList;

	var employeeList = employeeList.filter(
			item => {
					return (
						item.name.toLowerCase().includes(name.toLowerCase()) || 
						item.surname.toLocaleLowerCase().includes(name.toLocaleLowerCase()) ||
						item.dateOfBirth.toLocaleLowerCase().includes(name.toLocaleLowerCase()) ||
						item.jobTitle.toLocaleLowerCase().includes(name.toLocaleLowerCase())
					)
			}
		)
	return employeeList;
}

const getAllJobTitles = () => {
	var obj :JobTitleModel[] = 
	[
		{id: 1, description: 'Software Engineer'},
		{id: 2, description: 'Software Tester'},
		{id: 3, description: 'Database Administrator'},
		{id: 4, description: 'Business Analyst'}
	]
	setJobTitles(obj);
}

const saveNewEmployee = (employee: EmployeeModel): void => {
	console.log('obj : ', employee);
	setOrder('desc');
}

useEffect(() => {
	getAllJobTitles();
  }, [""]);

const emptyRows =
    page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

  return (
    <Box sx={{ width: '100%' }}>
        <Typography
			variant="h4"
			noWrap
			component="div"
            align='center'
			sx={{ my: 3}}
		>
			Employees
		</Typography>

		<Grid spacing={2} columns={12} sx={{mb:2}} container>
			<Grid item xs={6} container direction="row" justifyContent="center" alignItems="center">
				<TextField 
					id="outlined-basic" 
					label="Search" 
					variant="outlined" 
					size="small"
					onChange={findEmployee}
					value={searchValue}
				/>
			</Grid>
			<Grid item xs={6} container direction="row" justifyContent="center" alignItems="center">
				<Button variant="outlined" onClick={() => setOpenEmployeeDialog(true)}>
					Add New
				</Button>
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
						rowCount={employeesData.length}
					/>
					<TableBody>
						{stableSort(employeesData, getComparator(order, orderBy))
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
										<TableCell align="right">{row.surname}</TableCell>
										<TableCell align="right">{row.dateOfBirth}</TableCell>
										<TableCell align="right">{row.jobTitle}</TableCell>
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
				count={employeesData.length}
				rowsPerPage={rowsPerPage}
				page={page}
				onPageChange={handleChangePage}
				onRowsPerPageChange={handleChangeRowsPerPage}
			/>
      	</Paper>
	  	<EmployeeFormDialog 
	  		openDialog={openEmployeeDialog} 
	  		setOpenEmployeeDialogCallBack={setOpenEmployeeDialog} 
			jobTitles={jobTitles}
			saveNewEmployeeCallBack={saveNewEmployee}
		/>
    </Box>
  );
}

export default Employee;
