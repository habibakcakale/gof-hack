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
      projectForm: {
        title: "",
        description: "",
        tags: [
          {
            text: "Foo",
            color: "blue"
          }
        ]
      },
      activator: null,
      attach: null,
      colors: ["green", "purple", "indigo", "cyan", "teal", "orange"],
      editing: null,
      index: -1,
      items: [{ header: "Add a new tag" }],
      nonce: 1,
      menu: false,
      x: 0,
      search: null,
      y: 0
    };
  },
  watch: {
    "projectForm.tags": function(val, prev) {
      if (val.length === prev.length) return;

      this.projectForm.tags = val.map(v => {
        if (typeof v === "string") {
          v = {
            text: v,
            color: this.colors[this.nonce - 1]
          };

          this.items.push(v);

          this.nonce++;
        }

        return v;
      });
    }
  },
  mounted() {
    service.get("test").then(res => {
      console.log(res);
    });
  },
  methods: {
    handleProjectCreate() {
      this.$router.push(`project-summary`);
    },
    edit(index, item) {
      if (!this.editing) {
        this.editing = item;
        this.index = index;
      } else {
        this.editing = null;
        this.index = -1;
      }
    },
    filter(item, queryText, itemText) {
      if (item.header) return false;

      const hasValue = val => (val != null ? val : "");

      const text = hasValue(itemText);
      const query = hasValue(queryText);

      return (
        text
          .toString()
          .toLowerCase()
          .indexOf(query.toString().toLowerCase()) > -1
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
          Create New Project
        </v-btn>
        <v-dialog
          v-model="isOpenCreateProjectModal"
          persistent
          max-width="600px"
        >
          <v-card>
            <v-card-title>
              <span class="headline">Create Project</span>
            </v-card-title>
            <v-card-text>
              <v-container grid-list-md>
                <v-layout wrap>
                  <v-flex xs12>
                    <v-text-field
                      v-model="projectForm.title"
                      label="Project Name"
                      required
                    ></v-text-field>
                  </v-flex>
                  <v-flex xs12>
                    <v-textarea
                      v-model="projectForm.description"
                      label="Project Description"
                      required
                      auto-grow
                    ></v-textarea>
                  </v-flex>
                  <v-flex xs12>
                    <v-combobox
                      v-model="projectForm.tags"
                      :filter="filter"
                      :hide-no-data="!search"
                      :items="items"
                      :search-input.sync="search"
                      hide-selected
                      label="Add new tags"
                      multiple
                      small-chips
                    >
                      <template v-slot:no-data>
                        <v-list-tile>
                          <span class="subheading">Create</span>
                          <v-chip
                            :color="`${colors[nonce - 1]} lighten-3`"
                            label
                            small
                          >
                            {{ search }}
                          </v-chip>
                        </v-list-tile>
                      </template>
                      <template v-slot:selection="{ item, parent, selected }">
                        <v-chip
                          v-if="item === Object(item)"
                          :color="`${item.color} lighten-3`"
                          :selected="selected"
                          label
                          small
                        >
                          <span class="pr-2">
                            {{ item.text }}
                          </span>
                          <v-icon small @click="parent.selectItem(item)"
                            >close</v-icon
                          >
                        </v-chip>
                      </template>
                      <template v-slot:item="{ index, item }">
                        <v-list-tile-content>
                          <v-text-field
                            v-if="editing === item"
                            v-model="editing.text"
                            autofocus
                            flat
                            background-color="transparent"
                            hide-details
                            solo
                            @keyup.enter="edit(index, item)"
                          ></v-text-field>
                          <v-chip
                            v-else
                            :color="`${item.color} lighten-3`"
                            dark
                            label
                            small
                          >
                            {{ item.text }}
                          </v-chip>
                        </v-list-tile-content>
                        <v-spacer></v-spacer>
                        <v-list-tile-action @click.stop>
                          <v-btn icon @click.stop.prevent="edit(index, item)">
                            <v-icon>{{
                              editing !== item ? "edit" : "check"
                            }}</v-icon>
                          </v-btn>
                        </v-list-tile-action>
                      </template>
                    </v-combobox>
                  </v-flex>
                  <v-flex xs12>
                    <v-card>
                      <v-card-title>JSON FORMAT</v-card-title>
                      <pre>
                           {{ projectForm }}
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
              <v-btn color="blue darken-1" flat @click="handleProjectCreate"
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
