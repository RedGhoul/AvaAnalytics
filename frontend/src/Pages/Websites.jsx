import React, { Component } from "react";
import { view } from '@risingstack/react-easy-state';
import AuthProvider from '../Stores/AuthProvider';
import WebsiteProvider from '../Stores/WebsiteProvider';
import MaterialTable from "material-table";
import Button from '@material-ui/core/Button';
const actions = [
    {
        icon: 'edit',
        tooltip: 'Edit Index',
        onClick: (event, rowData) => {
            this.onEditClick(null, rowData._id);
        }
    },
    {
        icon: 'delete',
        tooltip: 'Delete Index',
        onClick: (event, rowData) => {
            this.onDeleteClick(null, rowData._id);
        }
    },
];
class Websites extends Component {

    constructor(props) {
        super(props);
        this.state = {
            canViewMetric: false
        }
    }
    render() {
        if (AuthProvider.isLoggedIn === false) {
            return (
                <div className="row">
                    <h2>Please Login</h2>
                </div>
            )
        } else {
            return (
                <div style={{ maxWidth: "100%" }}>
                    {this.state.canViewMetric == true ? <Button>
                        View Metrics
                    </Button> : null}

                    <MaterialTable
                        //actions={actions}
                        columns={[
                            { title: "Name", field: "name" },
                            { title: "SurName", field: "surname" },
                            { title: "BirthDate", field: "birthYear", type: "numeric" },
                            {
                                title: "birthCity",
                                field: "birthCity",
                                lookup: { 34: "İstanbul", 63: "Şanlıurfa" },
                            },
                            {
                                title: "Actions",
                                field: "actions",
                                //editable: 'never'
                            }
                        ]}

                        data={WebsiteProvider.data}
                        title="Web Sites"
                        options={{
                            selection: true
                        }}
                        onSelectionChange={(rows) => {
                            console.log(rows)
                            if (rows.length === 0) {
                                this.setState({ canViewMetric: false });
                            } else {
                                this.setState({ canViewMetric: true });

                            }
                        }}
                        editable={{
                            onRowAdd: newData =>
                                new Promise((resolve, reject) => {
                                    setTimeout(() => {
                                        WebsiteProvider.data =
                                            [...WebsiteProvider.data, newData]
                                        resolve();
                                    }, 1000)
                                }),

                        }}
                    />
                </div>
            );
        }
    }
}
export default view(Websites);
