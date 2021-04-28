import React, { Component } from "react";
import { Link } from "react-router-dom";
import {
  Breadcrumbs,
  Typography,
  Link as MaterialLink,
} from "@material-ui/core";
import NavigateNextIcon from "@material-ui/icons/NavigateNext";
import "../../Styles/_MP_Front-Internal.css";
import ListAltIcon from "@material-ui/icons/ListAlt";

// import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const Breadcrumb = (props) => {
  const isLink = (item) => {
    return item.length > 1;
  };

  return (
    <div className="content-head">
      <div className="border-bot">
        <div className="head-title">
          <div className="title-label-1">{props.titlePage}</div>
        </div>
      </div>

      <div className="border-bot">
        <Breadcrumbs
          maxItems={3}
          className="breadcrumb"
          separator={<NavigateNextIcon fontSize="small" />}
          aria-label="breadcrumb"
        >
          {props.items.map((item, index) => {
            if (isLink(item)) {
              return (
                <MaterialLink
                  className="breadcrumb-item"
                  key={item[0]}
                  color="inherit"
                  component={Link}
                  to={item[1]}
                >
                  {item[0]}
                </MaterialLink>
              );
            }

            return (
              <Typography
                className="breadcrumb-item"
                key={item[0]}
                color="textPrimary"
                // variant="caption"
              >
                {item[0]}
              </Typography>
            );
          })}
        </Breadcrumbs>
      </div>
    </div>
  );
};

export default Breadcrumb;
