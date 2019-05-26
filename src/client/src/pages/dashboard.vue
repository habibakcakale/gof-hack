<script>
import service from "@/utils/service";
export default {
  data() {
    return {
      isOpenCreateProjectModal: false,
      emailsSubscriptionChart: {
        data: {
          labels: [
            "Ja",
            "Fe",
            "Ma",
            "Ap",
            "Mai",
            "Ju",
            "Jul",
            "Au",
            "Se",
            "Oc",
            "No",
            "De"
          ],
          series: [[542, 443, 320, 780, 553, 453, 326, 434, 568, 610, 756, 895]]
        },
        options: {
          axisX: {
            showGrid: false
          },
          low: 0,
          high: 1000,
          chartPadding: {
            top: 0,
            right: 5,
            bottom: 0,
            left: 0
          }
        },
        responsiveOptions: [
          [
            "screen and (max-width: 640px)",
            {
              seriesBarDistance: 5,
              axisX: {
                labelInterpolationFnc: function(value) {
                  return value[0];
                }
              }
            }
          ]
        ]
      },
      currentTask: undefined,
      isLoading: false,
      selectedProject: {},
      entries: [],
      count: 0,
      search: "",
      estimateProjects: [],
      timeSpentProjects: [],
      completedProjects: [],
      newProjects: [],
      headers: [
        { text: "Jira Id", value: "key", align: "center", sortable: true },
        {
          text: "Project Name",
          value: "name",
          align: "center",
          sortable: true
        },
        { text: "Action", value: "key", align: "center", sortable: true }
      ]
    };
  },
  watch: {
    search() {
      // Items have already been requested
      if (this.isLoading) return;

      this.isLoading = true;

      // Lazily load input items
      service
        .post("jira/newProjects", {})
        .then(res => {
          const { values } = res;
          this.entries = values;
        })
        .catch(err => {
          console.log(err);
        })
        .finally(() => (this.isLoading = false));
    }
  },
  mounted() {
    service.post("dashboard/projectStatus", {}).then(res => {
      const {
        estimateProjects,
        timeSpentProjects,
        completedProjects,
        newProjects
      } = res;
      this.completedProjects = completedProjects;
      this.timeSpentProjects = timeSpentProjects;
      this.estimateProjects = estimateProjects;
      this.newProjects = newProjects;
    });
  },
  methods: {
    processed(item) {
      console.log(id);
      service.post("project/proceed", { projectId: item.projectId });
    },
    handleProjectEstimation() {
      this.$router.push(
        `project-summary/${this.selectedProject.id}?title=${
          this.selectedProject.name
        }&key=${this.selectedProject.key}`
      );
    }
  }
};
</script>

<template>
  <div style="max-width: 800px; margin-left: auto; margin-right:auto" class="py-3">
    <v-container grid-list-md fill-height>
      <v-layout justify-center>
        <v-card>
          <v-card-title>In Estimation</v-card-title>
          <v-data-table :headers="headers" :items="this.estimateProjects" hide-actions>
            <template v-slot:items="props">
              <tr>
                <td class="text-xs-center">{{ props.item.key }}</td>
                <td>{{ props.item.name }}</td>
                <td class="text-xs-center">
                  <v-btn fab dark color="teal" small @click="processed(props.item)">
                    <v-icon dark>arrow_forward</v-icon>
                  </v-btn>
                </td>
              </tr>
            </template>
          </v-data-table>
        </v-card>
        <v-spacer></v-spacer>
        <v-card>
          <v-card-title>In Progress</v-card-title>
          <v-data-table :headers="headers" :items="this.timeSpentProjects" hide-actions>
            <template v-slot:items="props">
              <tr>
                <td class="text-xs-center">{{ props.item.key }}</td>
                <td>{{ props.item.name }}</td>
                <td class="text-xs-center">
                  <v-btn fab dark color="teal" small @click="processed(props.item)">
                    <v-icon dark>arrow_forward</v-icon>
                  </v-btn>
                </td>
              </tr>
            </template>
          </v-data-table>
        </v-card>
      </v-layout>
    </v-container>
    <v-container>
      <v-layout justify-center align-center>
        <v-dialog v-model="isOpenCreateProjectModal" persistent max-width="600px">
          <v-card>
            <v-card-title>
              <span class="headline">Esimate A Project</span>
            </v-card-title>
            <v-card-text>
              <v-container grid-list-md>
                <v-layout wrap>
                  <v-flex xs12>
                    <v-autocomplete
                      v-model="selectedProject"
                      :items="entries"
                      :loading="isLoading"
                      :search-input.sync="search"
                      hide-no-data
                      hide-selected
                      item-text="name"
                      item-value="name"
                      label="JIRA Projects"
                      placeholder="Start typing to Search"
                      return-object
                    ></v-autocomplete>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="warning darken-1" flat @click="isOpenCreateProjectModal = false">Close</v-btn>
              <v-btn color="success darken-1" @click="handleProjectEstimation">Go!</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-layout>
      <v-btn
        absolute
        dark
        fab
        class="mb-5"
        bottom
        right
        color="blue"
        @click="isOpenCreateProjectModal = true"
      >
        <v-icon>fa-plus</v-icon>
      </v-btn>
    </v-container>
  </div>
</template>
