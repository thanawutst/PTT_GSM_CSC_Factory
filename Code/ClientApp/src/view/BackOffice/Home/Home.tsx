import React, { useEffect, useState } from "react";
import ContentWrapper, {
  FormWrapper,
  FormButtons,
} from "src/components/Commons/style/ContentWrapper";
import { Button, Typography, Grid } from "@material-ui/core";
import { FormatListBulleted } from "@material-ui/icons";
import { teal, purple, lightBlue, pink } from "@material-ui/core/colors";
import { makeStyles, Theme } from "@material-ui/core/styles";
import { Link } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Tabs from "@material-ui/core/Tabs";
import Tab from "@material-ui/core/Tab";
import Box from "@material-ui/core/Box";

interface TabPanelProps {
  children?: React.ReactNode;
  index: any;
  value: any;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box p={3}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

function a11yProps(index: any) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

const useStyles = makeStyles((theme: Theme) => ({
  root: {
    flexGrow: 1,
    backgroundColor: theme.palette.background.paper,
  },
}));

export default function Home() {
  const fontsize = 200;
  const useStyleGreen = makeStyles({
    root: {
      color: teal[200],
      backgroundColor: teal[500],
    },
  });
  const useStylePurple = makeStyles({
    root: {
      color: purple[100],
      backgroundColor: purple[400],
    },
  });
  const useStyleBlue = makeStyles({
    root: {
      color: lightBlue[100],
      backgroundColor: lightBlue[600],
    },
  });
  const useStyleRed = makeStyles({
    root: {
      color: pink[100],
      backgroundColor: pink[300],
    },
  });
  const classesGreen = useStyleGreen();
  const classesPurple = useStylePurple();
  const classesBlue = useStyleBlue();
  const classesRed = useStyleRed();

  const classes = useStyles();
  const [value, setValue] = React.useState(0);

  const handleChange = (event: React.ChangeEvent<{}>, newValue: number) => {
    setValue(newValue);
  };

  return (
    <>
      <Typography variant="h2" align="center">
        {"Home"}
      </Typography>
      {/* <Typography variant="h2" align="center">
                    {"Softthai Key: "}{`${process.env.REACT_APP_JWT_KEY}`}
                    {console.log("cutuser", cutuser)}
                </Typography> */}
      <Grid spacing={2} container>
        <Grid
          item
          xs={12}
          container
          direction="row"
          justify="center"
          alignItems="center"
        >
          <FormButtons>
            <Link to="/" key="/">
              <Button
                variant="outlined"
                className={classesGreen.root}
                title={"My Task"}
              >
                <FormatListBulleted
                  style={{ fontSize: fontsize }}
                ></FormatListBulleted>
              </Button>
            </Link>
            <Link to="/" key="/">
              <Button
                variant="outlined"
                className={classesPurple.root}
                title={"AcceptWork"}
              >
                <FormatListBulleted
                  style={{ fontSize: fontsize }}
                ></FormatListBulleted>
              </Button>
            </Link>
            <Link to="/" key="/">
              <Button
                variant="outlined"
                className={classesBlue.root}
                title={"Travel"}
              >
                <FormatListBulleted
                  style={{ fontSize: fontsize }}
                ></FormatListBulleted>
              </Button>
            </Link>
            <Link to="/" key="/">
              <Button
                variant="outlined"
                className={classesRed.root}
                title={"Allowance"}
              >
                <FormatListBulleted
                  style={{ fontSize: fontsize }}
                ></FormatListBulleted>
              </Button>
            </Link>
          </FormButtons>
        </Grid>
      </Grid>
    </>
  );
}
