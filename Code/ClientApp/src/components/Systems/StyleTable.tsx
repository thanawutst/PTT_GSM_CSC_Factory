import { makeStyles, createMuiTheme } from '@material-ui/core';
import { CSSProperties } from '@material-ui/core/styles/withStyles';
export const getMuiTheme = () => createMuiTheme({
    overrides: {

        MuiGrid: {
            root: {
                border: "1px solid #d7d7d7 !important",
                borderBottom: 0,
                backgroundColor: '#83c8fe !important',
                fontSize: 13,
                textAlign: "center",
                display: "table-cell"

            }
        },
        MuiTableCell: {
            // root: {
            //     paddingLeft: 10,
            //     paddingRight: 10,
            //     paddingTop: 10,
            //     paddingBottom: 10,
            //     //"&:last-child": {
            //     //    paddingRight: 0
            //     //}
            // },
            // head: {
            //     border: "1px solid #d7d7d7 !important",
            //     borderBottom: 0,
            //     // backgroundColor: '#83c8fe !important',
            //     fontSize: 13,
            //     "& > span": {
            //         display: "inline"
            //     },
            //     textAlign: "center",
            // },
            // body: {
            //     borderBottom: "none",
            //     border: "1px solid #d7d7d7 !important",
            //     "& > div&$MuiTypography-root": {
            //         padding: '30px'
            //     },
            // },
            // footer: {
            //     borderBottom: "none",
            //     // backgroundColor: '#efefef !important',
            //     border: "1px solid #d7d7d7 !important"
            // }
        },
        MuiButton: {
            root: {
                margin: "0 !important",
                padding: "0 !important",
            }
        },
        MuiPaper: {
            elevation4: {
                boxShadow: "none"
            }
        }
    },
});

export const classToLabel = {
    Active: { color: "green" } as CSSProperties,
    Inactive: { color: "red" } as CSSProperties,
    icon: { color: "white" } as CSSProperties,
    bticon: {
        backgroundColor: "#2196f3",
        '&:hover': { backgroundColor: "#218af3" },
    } as CSSProperties,
    bticonEdit: {
        backgroundColor: "#72cff8",
        '&:hover': { backgroundColor: "#72cff8" },
    } as CSSProperties,
    bticonDel: {
        backgroundColor: "#ff0000",
        '&:hover': { backgroundColor: "#ff0000" },
    } as CSSProperties,
    Alignicon: { textAlign: "center", } as CSSProperties,
    setHead: { textAlign: "center", } as CSSProperties,
    FontHeadTable: { textAlign: "center", fontWeight: 'bold' } as CSSProperties
};