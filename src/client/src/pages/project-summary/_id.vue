<script lang="js">
import service from "@/utils/service";
  export default  {
    name: 'ProjectSummary',
    props: [],
    data() {
      return {
      currentTask: '',
      searchItems:[],
      issues: [],
      breadcrumbs: [
        {
          text: 'DashBoard',
          to: '/dashboard'
        },
         {
          text: this.$route.query.title
        },
         {
          text: this.$route.query.key
        },
      ],
       expand: false,
        headers: [
          { text: 'Issue Type', value: 'fields.issueType.name', align: 'center', sortable: false },
          { text: 'Key', value: 'key',  align: 'center',  sortable: false  },
          { text: 'Summary', value: 'fields.summary',  align: 'center',  sortable: false  },
          { text: 'Priority', value: 'fields.priority.name',  align: 'center',  sortable: false  },
          { text: 'Time Spent', value: 'fields.timeSpent',  align: 'center',  sortable: false  },
          { text: 'Time Estimation', value: 'timeestimate',  align: 'center',  sortable: false  },

        ],
        subTaskHeaders: [
          { text: 'Title', value: 'title', align: 'center', sortable: false },
          { text: 'Description', value: 'description',  align: 'center',  sortable: false  },
          { text: 'User Role', value: 'userRole',  align: 'center',  sortable: false  },
          { text: 'User Level', value: 'userRole',  align: 'center',  sortable: false  },
          { text: 'Time Spent', value: 'timeestimate',  align: 'center',  sortable: false  },
          { text: 'JIRA Link', value: 'timeestimate',  align: 'center',  sortable: false  }
        ],
        estimationMethods: [
        "FastForest",
        "FastTree",
        "FastTreeTweedie",
        "Gam",
        "LbfgsPoissonRegression",
        "OnlineGradientDescent",
        "Sdca"
        ],
        selectedEstimationMethod: "FastForest"
      }
    },
    mounted() {
      this.getCurrentProjectTasks()
    },
    methods: {
      getCurrentProjectTasks() {
        const projectId = this.$route.params.id;
        service.post('jira/tasks', { projectId }).then(({total, issues}) => {
            this.total = total;
            this.issues = issues;
        })
      },
      setCurrentTask(task) {
         service.post('search', {
          issue:task,
          method: this.selectedEstimationMethod
        }).then(({searchItems})=>{
          this.searchItems = searchItems;
        });
        this.currentTask = task;
      },
      updateCurrentTaskWithThisEstimation() {

      }
    }
}
</script>

<template>
  <section class="project_summary">
    <v-container class="project_container">
      <v-breadcrumbs :items="breadcrumbs" divider="-">
        <template v-slot:item="props">
          <div v-if="props.item.to">
            <router-link :to="props.item.to">{{ props.item.text }}</router-link>
          </div>
          <div v-else>
            <span>{{ props.item.text }}</span>
          </div>
        </template>
      </v-breadcrumbs>
      <v-toolbar class="py-3" flat color="white">
        <v-toolbar-title>{{ $route.query.title }}</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-flex xs12 sm6 d-flex align-center>
          <v-select
            v-model="selectedEstimationMethod"
            :items="estimationMethods"
            label="Chose a estiomation method"
            hint="Estimations are calculated with the regression algorithm defined here."
            persistent-hint
          ></v-select>
        </v-flex>
      </v-toolbar>
      <v-data-table
        :headers="headers"
        :items="issues"
        :expand="expand"
        hide-actions
      >
        <template v-slot:items="props">
          <tr
            :class="{
              'active-task': currentTask && currentTask.id == props.item.id
            }"
            style="cursor:pointer"
            @click="
              (props.expanded = !props.expanded), setCurrentTask(props.item)
            "
          >
            <td class="text-xs-center">
              {{ props.item.fields.issueType.name }}
            </td>
            <td class="text-xs-center">{{ props.item.key }}</td>
            <td class="text-xs-center">{{ props.item.fields.summary }}</td>
            <td class="text-xs-center">
              {{ props.item.fields.priority.name }}
            </td>
            <td class="text-xs-center">
              {{ Number(props.item.fields.timeSpent) }}
            </td>
          </tr>
        </template>
        <template v-slot:expand="props">
          <v-card class="py-4" flat>
            <!-- <p class="my-2">
              <pre>{{ currentTask | json }}</pre>
            </p> -->
            <div style="display: flex; align-items: center;" class="mx-5">
              <h3 class="my-3">Choose a similar task</h3>
              <v-spacer></v-spacer>
            </div>
            <v-card class="mx-4">
              <v-data-table
                :headers="subTaskHeaders"
                :items="searchItems"
                hide-actions
              >
                <template v-slot:items="props">
                  <tr style="cursor:pointer">
                    <td class="text-xs-center">
                      {{ props.item.title }}
                    </td>
                    <td class="text-xs-left py-4">
                      {{
                        props.item.description
                          ? props.item.description
                          : "No description added"
                      }}
                    </td>
                    <td class="text-xs-center">{{ props.item.userRole }}</td>
                    <td class="text-xs-center">{{ props.item.userLevel }}</td>
                    <td class="text-xs-center">
                      {{ props.item.estimate }}
                    </td>
                    <td class="text-xs-center">
                      <a target="_blank" :href="props.item">
                        <v-icon color="primary" small>
                          link
                        </v-icon>
                      </a>
                      {{ props.item }}
                    </td>
                  </tr>
                </template>
              </v-data-table>
            </v-card>
          </v-card>
        </template>
      </v-data-table>

      <v-container>
        <v-spacer></v-spacer>
      </v-container>

      <v-toolbar flat color="white">
        <v-spacer></v-spacer>
        <v-btn>Complete!</v-btn>
        <v-spacer></v-spacer>
      </v-toolbar>

      <!--  <v-btn class="mb-5" absolute dark fab bottom right color="blue">
        <v-icon>fa-plus</v-icon>
      </v-btn> -->
    </v-container>
  </section>
</template>

<style lang="scss">
.project_container {
  max-width: 1200px;
  margin-left: auto;
  margin-right: auto;
}
.active-task {
  background: #c8e6c9 !important;
  font-weight: bold !important;
  &:hover {
    background: #c8e6c9 !important;
    font-weight: bold !important;
  }
}
</style>
