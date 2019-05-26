<script lang="js">
import service from "@/utils/service";
  export default  {
    name: 'ProjectSummary',
    props: [],
    data() {
      return {
      currentTaskId: '',
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
        ],
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
      setCurrentTask(id) {
        this.currentTaskId = id;
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
      <v-toolbar flat color="white">
        <v-toolbar-title>{{ $route.query.title }}</v-toolbar-title>
        <v-spacer></v-spacer>
      </v-toolbar>
      <v-data-table
        :headers="headers"
        :items="issues"
        :expand="expand"
        hide-actions
      >
        <template v-slot:items="props">
          <tr
            style="cursor:pointer"
            @click="
              (props.expanded = !props.expanded), setCurrentTask(props.item.id)
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
          <v-card flat>
            <p class="text-xs-center my-2">
              We have current id {{ currentTaskId }}, so we can fetch similar
              ones
            </p>
          </v-card>
        </template>
      </v-data-table>

      <!--  <v-btn class="mb-5" absolute dark fab bottom right color="blue">
        <v-icon>fa-plus</v-icon>
      </v-btn> -->
    </v-container>
  </section>
</template>

<style scoped lang="scss">
.project_container {
  max-width: 1200px;
  margin-left: auto;
  margin-right: auto;
}
</style>
