<script lang="js">
import service from "@/utils/service";
  export default  {
    name: 'ProjectSummary',
    filters: {
      timeInHours(d) {
        d = Number(d);
      const h = Math.floor(d / 3600);
      const m = Math.floor(d % 3600 / 60);
      const s = Math.floor(d % 3600 % 60);

      const hDisplay = h > 0 ? h + (h == 1 ? " hour, " : " hours, ") : "";
      const mDisplay = m > 0 ? m + (m == 1 ? " minute, " : " minutes, ") : "";
      const sDisplay = s > 0 ? s + (s == 1 ? " second" : " seconds") : "";
      return hDisplay + mDisplay + sDisplay;

    }
    },
    props: [],
    data() {
      return {
      estimationTimeValue: {},
      currentTask: '',
      searchItems:[],
      issues: [],
      roles: [
        { label: "Frontend Developer", value: "FD" },
        { label: "Backend Developer", value: "BD" },
        { label: "Quality Assurance", value: "QA" },
        { label: "Bussiness Analyst", value: "BA" },
        { label: "Solution Architect", value: "SA" },
        { label: "Project Manager", value: "PM" }
      ],
      levels: [
        { label: "Junior 0", value: "J0" },
        { label: "Junior 1", value: "J1" },
        { label: "Junior 2", value: "J2" },
        { label: "Intermediate 1", value: "I1" },
        { label: "Intermediate 2", value: "I2" },
        { label: "Senior 1", value: "S1" },
        { label: "Senior 2", value: "S2" }
      ],
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
          { text: 'Estimated Time', value: 'timeestimate',  align: 'center',  sortable: false  },
          { text: 'Time Estimation', value: 'timeestimate',  align: 'center',  sortable: false  },

        ],
        subTaskHeaders: [
          { text: 'Title', value: 'title', align: 'center', sortable: false },
          { text: 'Description', value: 'description',  align: 'center',  sortable: false  },
          { text: 'User Role', value: 'userRole',  align: 'center',  sortable: false  },
          { text: 'User Level', value: 'userRole',  align: 'center',  sortable: false  },
          { text: 'Time Spent', value: 'timeestimate',  align: 'center',  sortable: false  },
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
        }).then(({searchItems, estimate})=>{
          this.searchItems = searchItems;
          this.currentEstimation = estimate;
        });
        this.currentTask = task;
      },
      handleTimeUpdate(id) {
        const issueIdOrKey = id

        const estimate = Number(this.estimationTimeValue[id]) * 60 ;
        service.post('jira/updateestimate', {
         estimate,
          issueIdOrKey
        }).then(({isSuccess}) => {
            if(isSuccess) {
            this.getCurrentProjectTasks()
            this.this.estimationTimeValue[id] = ''
            }
        })
      },
      processed(){
        service.post('project/proceed', {
          projectId:this.$route.params.id,
          jiraId:  this.$route.params.id,
          state:"Initial"
        }).then(()=> {
          this.$toasted.show("Project created successfully",{
            type:"success",
            duration: 3000,
            position: "top-center"
          });
          this.$router.push("/dashboard");
        })
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
      <v-data-table :headers="headers" :items="issues" :expand="expand" hide-actions>
        <template v-slot:items="props">
          <tr
            :class="{
              'active-task': currentTask && currentTask.id == props.item.id
            }"
            style="cursor:pointer"
          >
            <td
              class="text-xs-center"
              @click="(props.expanded = !props.expanded), setCurrentTask(props.item)"
            >{{ props.item.fields.issueType.name }}</td>
            <td
              class="text-xs-center"
              @click="(props.expanded = !props.expanded), setCurrentTask(props.item)"
            >{{ props.item.key }}</td>
            <td
              class="text-xs-center"
              @click="(props.expanded = !props.expanded), setCurrentTask(props.item)"
            >{{ props.item.fields.summary }}</td>
            <td
              class="text-xs-center"
              @click="(props.expanded = !props.expanded), setCurrentTask(props.item)"
            >{{ props.item.fields.priority.name }}</td>
            <td
              class="text-xs-center"
              @click="(props.expanded = !props.expanded), setCurrentTask(props.item)"
            >{{ props.item.fields.timeOriginalEstimate | timeInHours }}</td>
            <td class="text-xs-center">
              <v-text-field
                v-model.number="estimationTimeValue[props.item.id]"
                label="Please input your time hourly"
              >
                <template v-slot:append-outer>
                  <v-btn
                    flat
                    small
                    class
                    color="success"
                    @click="handleTimeUpdate(props.item.id)"
                  >update</v-btn>
                </template>
              </v-text-field>
            </td>
          </tr>
        </template>
        <template v-slot:expand="props">
          <v-card flat>
            <p class="mt-5 px-3" style="font-size: 20px; text-align: center;">
              According to the training data constructed with
              <strong>{{ selectedEstimationMethod }}</strong>
              algorithm, our
              current estimation is: {{ currentEstimation | timeInHours }}
            </p>
          </v-card>
          <v-divider></v-divider>
          <v-card class="py-4" flat>
            <div style="display: flex; align-items: center;" class="mx-5">
              <h3 class="my-3">Similar tasks that can helps you determine estimation:</h3>
              <v-spacer></v-spacer>
            </div>
            <v-card class="mx-4">
              <v-data-table :headers="subTaskHeaders" :items="searchItems" hide-actions>
                <template v-slot:items="props">
                  <tr style="cursor:pointer">
                    <td class="text-xs-center">{{ props.item.title }}</td>
                    <td class="text-xs-left py-4">
                      {{
                      props.item.description
                      ? props.item.description
                      : "No description added"
                      }}
                    </td>
                    <td class="text-xs-center">{{ roles[props.item.userRole]["label"] }}</td>
                    <td class="text-xs-center">{{ levels[props.item.userLevel]["label"] }}</td>
                    <td class="text-xs-center">{{ props.item.estimate | timeInHours }}</td>
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
        <v-btn @click="processed">Complete!</v-btn>
        <v-spacer></v-spacer>
      </v-toolbar>

      <!--  <v-btn class="mb-5" absolute dark fab bottom right color="blue">
        <v-icon>fa-plus</v-icon>
      </v-btn>-->
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
