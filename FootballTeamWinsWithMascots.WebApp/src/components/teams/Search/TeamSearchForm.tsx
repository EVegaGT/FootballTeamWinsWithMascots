import { Box, Button, Checkbox, FormControl, Grid, InputAdornment, InputLabel, ListItemText, MenuItem, OutlinedInput, Select, TextField, type SelectChangeEvent } from "@mui/material";
import SearchIcon from '@mui/icons-material/Search';
import { useState } from "react";
import { Api, type SearchTeamsQuery, type TeamDtoPagedResult } from "../../../api/generated/client";

interface Props {
    setResult: React.Dispatch<React.SetStateAction<TeamDtoPagedResult | null>>
    loading: boolean,
    setLoading: React.Dispatch<React.SetStateAction<boolean>>
}

const api = new Api()
const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
    PaperProps: {
        style: {
            maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
            width: 250,
        },
    },
};

const validColumns = [
    'All',
    'Name',
    'Mascot',
];

export default function TeamSearchForm({ setResult, loading, setLoading }: Props) {
    const [query, setQuery] = useState("");
    const [column, setColumn] = useState<string[]>([]);
   
    const handleChange = (event: SelectChangeEvent<typeof column>) => {
        const { value } = event.target;
        const newValue = typeof value === "string" ? value.split(",") : (value as string[]);

        const hadAll = column.includes("All");
        const hasAllNow = newValue.includes("All");

        if (hasAllNow && !hadAll) {
            setColumn(["All"]);
        } else if (hasAllNow && hadAll) {
            const withoutAll = newValue.filter(v => v !== "All");
            setColumn(withoutAll);
        } else {
            setColumn(newValue);
        }
    };

    const onSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        try {
            const body: SearchTeamsQuery = {
                query: query.trim(),
                columns: !column?.length || column.includes("All") //If we don't select any column, we will send all the valid columns
                    ? validColumns.filter((v) => v !== "All")
                    : column,
                page: 1,
                pageSize: 200, //The enpoint support pagination, but for now, we won't implement it yet
            };

            const { data } = await api.api.teamSearchCreate(body);
            setResult(data);
        } catch (err) {
            console.error(err);
            alert("Search failed. Check console for details.");
        } finally {
            setLoading(false);
        }
    };
    return (
        <form onSubmit={onSubmit}>
            <Box sx={{ display: "flex", justifyContent: "space-between", mt: 4 }}>
                <Grid
                    container
                    spacing={2}
                    alignItems="center"
                    justifyContent="center"
                    sx={{
                        maxWidth: 900,
                        width: "100%",
                    }}
                >
                    <Grid size={{ xs: 12, md: 7, sm: 7 }}>
                        <FormControl fullWidth variant="outlined">
                            <OutlinedInput
                                size="medium"
                                id="search-input"
                                placeholder="Search..."
                                startAdornment={
                                    <InputAdornment position="start">
                                        <SearchIcon color="action" />
                                    </InputAdornment>
                                }
                                onChange={e => setQuery(e.target.value)}
                            />
                        </FormControl>
                    </Grid>
                    <Grid size={{ xs: 12, md: 4, sm: 4 }}>
                        <FormControl fullWidth>
                            <InputLabel id="filter-label">Filter Column</InputLabel>
                            <Select
                                labelId="filter-label"
                                multiple
                                value={column}
                                onChange={handleChange}
                                input={<OutlinedInput label="Filter Column" placeholder="Select Columns" />}
                                renderValue={(selected) => selected.join(", ")}
                                MenuProps={MenuProps}
                            >
                                {validColumns.map((validColumn) => (
                                    <MenuItem key={validColumn} value={validColumn}>
                                        <Checkbox checked={column.includes(validColumn)} />
                                        <ListItemText primary={validColumn} />
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid size={{ xs: 12, md: 1, sm: 1 }}>
                        <Button
                            variant="contained"
                            size="small"
                            sx={{ height: "46px", minWidth: "100px" }}
                            type="submit"
                            disabled={loading}
                        >
                            {loading ? "Searching..." : "Search"}
                        </Button>
                    </Grid>
                </Grid>
            </Box>
        </form>
    );
}