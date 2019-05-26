<script>
import TheChartCard from "@/components/TheChartCard";
import service from "@/utils/service";
export default {
  components: {
    TheChartCard
  },
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
      isLoading: false,
      selectedProject: {},
      entries: [],
      count: 0,
      search: ""
    };
  },
  watch: {
    search() {
      // Items have already been requested
      if (this.isLoading) return;

      this.isLoading = true;

      // Lazily load input items
      service
        .post("jira/projects", {})
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
  mounted() {},
  methods: {
    handleProjectEstimation() {
      this.$router.push(
        `project-summary/${this.selectedProject.id}?title=${
          this.selectedProject.name
        }&key=${this.selectedProject.key}"`
      );
    }
  }
};
</script>

<template>
  <div class="py-3">
    <h3 class="text-xs-center">Some Insights</h3>
    <v-container grid-list-md fill-height>
      <v-layout justify-center align-center>
        <v-flex v-for="i in 4" :key="i" md4 text-xs-center>
          <the-chart-card
            :data="emailsSubscriptionChart.data"
            :options="emailsSubscriptionChart.options"
            color="white"
            type="Line"
          >
            <h3 class="title font-weight-light">Completed Tasks</h3>
            <p class="category d-inline-flex font-weight-light">
              Last Last Campaign Performance
            </p>
          </the-chart-card>
        </v-flex>
      </v-layout>
    </v-container>
    <v-container>
      <v-layout justify-center align-center>
        <v-btn @click="isOpenCreateProjectModal = true">
          Estimate A Project
        </v-btn>
        <v-dialog
          v-model="isOpenCreateProjectModal"
          persistent
          max-width="600px"
        >
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
                  <v-flex xs12>
                    <v-card>
                      <v-card-title>JSON FORMAT</v-card-title>
                      <pre>
                           {{ search }}
                           {{ selectedProject }}
                      </pre>
                    </v-card>
                  </v-flex>
                </v-layout>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="blue darken-1"
                flat
                @click="isOpenCreateProjectModal = false"
                >Close</v-btn
              >
              <v-btn color="blue darken-1" flat @click="handleProjectEstimation"
                >Create</v-btn
              >
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
